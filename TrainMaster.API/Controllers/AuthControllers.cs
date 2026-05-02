using TrainMaster.Application.DTOs;
using TrainMaster.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TrainMaster.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private const string RefreshTokenCookie = "refresh_token";

    [HttpPost("register")]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
        var result = await authService.RegisterAsync(request, ct);
        if (!result.IsSuccess)
            return BadRequest(new { message = result.Error });

        AppendRefreshTokenCookie(result.Data!.RefreshToken, result.Data.RefreshTokenExpiresAt);
        return StatusCode(201, result.Data.Response);
    }

    [HttpPost("login")]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var result = await authService.LoginAsync(request, ct);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, new { message = result.Error });

        AppendRefreshTokenCookie(result.Data!.RefreshToken, result.Data.RefreshTokenExpiresAt);
        return Ok(result.Data.Response);
    }

    /// <summary>
    /// Emite novo par de tokens usando o access token expirado (header)
    /// e o refresh token do HttpOnly cookie (automático pelo browser).
    /// </summary>
    [HttpPost("refresh")]
    [ProducesResponseType<AuthResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(CancellationToken ct)
    {
        var accessToken = Request.Headers.Authorization.ToString().Replace("Bearer ", "");
        var refreshToken = Request.Cookies[RefreshTokenCookie];

        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            return Unauthorized(new { message = "Missing tokens." });

        var result = await authService.RefreshAsync(accessToken, refreshToken, ct);
        if (!result.IsSuccess)
            return StatusCode(result.StatusCode, new { message = result.Error });

        AppendRefreshTokenCookie(result.Data!.RefreshToken, result.Data.RefreshTokenExpiresAt);
        return Ok(result.Data.Response);
    }

    /// <summary>
    /// Retorna os dados do usuário autenticado extraídos do JWT — sem bater no banco.
    /// </summary>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType<MeResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Me()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var name   = User.FindFirstValue(ClaimTypes.Name);
        var email  = User.FindFirstValue(ClaimTypes.Email);
        var role   = User.FindFirstValue(ClaimTypes.Role);

        return Ok(new MeResponse(Guid.Parse(userId!), name!, email!, role!));
    }

    [Authorize]
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout(CancellationToken ct)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        await authService.LogoutAsync(userId, ct);

        Response.Cookies.Delete(RefreshTokenCookie);
        return NoContent();
    }

    // ── Helper ───────────────────────────────────────────────────────────────

    private void AppendRefreshTokenCookie(string refreshToken, DateTime expires)
    {
        Response.Cookies.Append(RefreshTokenCookie, refreshToken, new CookieOptions
        {
            HttpOnly = true,                   // JavaScript não consegue ler
            Secure = true,                     // apenas HTTPS
            SameSite = SameSiteMode.Strict,    // proteção contra CSRF
            Expires = expires
        });
    }
}

// ── DTOs locais do controller ────────────────────────────────────────────────
public record MeResponse(Guid Id, string Name, string Email, string Role);
