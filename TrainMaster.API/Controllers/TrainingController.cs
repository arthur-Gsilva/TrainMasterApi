using TrainMaster.Application.DTOs;
using TrainMaster.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrainMaster.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]

public class TrainingController(ITrainingService trainingService) : AppControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<TrainingResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await trainingService.GetAllAsync(ct);
        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<IEnumerable<TrainingResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Find(Guid id, CancellationToken ct)
    {
        var result = await trainingService.GetByIdAsync(id, ct);
        return Ok(result.Data);
    }

    [HttpPost]
    [ProducesResponseType<IEnumerable<TrainingResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateTrainingResponse request, CancellationToken ct)
    {
        var result = await trainingService.CreateAsync(CurrentUserId, request, ct);
        return Ok(result.Data);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<IEnumerable<TrainingResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTrainingResponse request, CancellationToken ct)
    {
        var result = await trainingService.UpdateAsync(id, request, ct);
        return Ok(result.Data);
    }
}

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TrainingWorkoutController(ITrainingWorkoutService trainingWorkoutService) : AppControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<TrainingWorkoutResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await trainingWorkoutService.GetAllAsync(ct);
        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<IEnumerable<TrainingWorkoutResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Find(Guid id, CancellationToken ct)
    {
        var result = await trainingWorkoutService.GetByIdAsync(id, ct);
        return Ok(result.Data);
    }

    [HttpPost]
    [ProducesResponseType<IEnumerable<TrainingWorkoutResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateTrainingWorkout request, CancellationToken ct)
    {
        var result = await trainingWorkoutService.CreateAsync(request, ct);
        return Ok(result.Data);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<IEnumerable<TrainingWorkoutResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTrainingWorkout request, CancellationToken ct)
    {
        var result = await trainingWorkoutService.UpdateAsync(id, request, ct);
        return Ok(result.Data);
    }
}

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TrainingSessionController(ITrainingSessionService trainingSessionService) : AppControllerBase
{
    [HttpGet]
    [ProducesResponseType<IEnumerable<TrainingSessionResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var result = await trainingSessionService.GetAllAsync(ct);
        return Ok(result.Data);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<IEnumerable<TrainingSessionResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Find(Guid id, CancellationToken ct)
    {
        var result = await trainingSessionService.GetByIdAsync(id, ct);
        return Ok(result.Data);
    }

    [HttpPost]
    [ProducesResponseType<IEnumerable<TrainingSessionResponse>>(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateTrainingSession request, CancellationToken ct)
    {
        var result = await trainingSessionService.CreateAsync(CurrentUserId, request, ct);
        return Ok(result.Data);
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType<IEnumerable<TrainingSessionResponse>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTrainingSession request, CancellationToken ct)
    {
        var result = await trainingSessionService.UpdateAsync(id, request, ct);
        return Ok(result.Data);
    }
}