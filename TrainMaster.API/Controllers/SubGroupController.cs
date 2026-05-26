using TrainMaster.Application.DTOs;
using TrainMaster.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SubGroupController(ISubGroupService subGroupService) : AppControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<SubGroupResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await subGroupService.GetAllAsync(ct);
        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<IEnumerable<SubGroupResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Find(Guid id, CancellationToken ct)
    {
        var result = await subGroupService.GetByIdAsync(id, ct);
        return Ok(result.Data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]     
    [ProducesResponseType<IEnumerable<SubGroupResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateSubGroupRequest request, CancellationToken ct)
    {
        var result = await subGroupService.CreateAsync(request, ct);
        return Ok(result.Data);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]     
    [ProducesResponseType<IEnumerable<SubGroupResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSubGroupRequest request, CancellationToken ct)
    {
        var result = await subGroupService.UpdateAsync(id, request, ct);
        return Ok(result.Data);
    }
}