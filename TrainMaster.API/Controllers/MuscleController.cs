using TrainMaster.Application.DTOs;
using TrainMaster.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainMaster.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MuscleController(IMuscleService muscleService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<MuscleResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await muscleService.GetAllAsync(ct);
        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<IEnumerable<MuscleResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Find(Guid id, CancellationToken ct)
    {
        var result = await muscleService.GetByIdAsync(id, ct);
        return Ok(result.Data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]    
    [ProducesResponseType<IEnumerable<MuscleResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody]CreateMuscleRequest request, CancellationToken ct)
    {
        var result = await muscleService.CreateAsync(request, ct);
        return Ok(result.Data);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]   
    [ProducesResponseType<IEnumerable<MuscleResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMuscleRequest request, CancellationToken ct)
    {
        var result = await muscleService.UpdateAsync(id, request, ct);
        return Ok(result.Data);
    }
}