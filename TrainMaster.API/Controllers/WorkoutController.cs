using TrainMaster.Application.DTOs;
using TrainMaster.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainMaster.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class WorkoutController(IWorkoutService workoutService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<WorkoutResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await workoutService.GetAllAsync(ct);
        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<IEnumerable<WorkoutResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Find(Guid id, CancellationToken ct)
    {
        var result = await workoutService.GetByIdAsync(id, ct);
        return Ok(result.Data);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]    
    [ProducesResponseType<IEnumerable<WorkoutResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateWorkoutRequest request, CancellationToken ct)
    {
        var result = await workoutService.CreateAsync(request, ct);
        return Ok(result.Data);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]   
    [ProducesResponseType<IEnumerable<WorkoutResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWorkoutRequest request, CancellationToken ct)
    {
        var result = await workoutService.UpdateAsync(id, request, ct);
        return Ok(result.Data);
    }
}