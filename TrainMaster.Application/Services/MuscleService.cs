using TrainMaster.Application.Common;
using TrainMaster.Application.Interfaces;
using TrainMaster.Application.DTOs;
using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;

using TrainMaster.Application.Mappings;


namespace TrainMaster.Application.Services;

public class MuscleService(
    IUnitOfWork unitOfWork
): IMuscleService
{
    public async Task<ServiceResult<IEnumerable<MuscleResponse>>> GetAllAsync(CancellationToken ct = default)
    {
        var muscles = await unitOfWork.Muscles.GetAllAsync(ct);
        return ServiceResult<IEnumerable<MuscleResponse>>.Success(muscles.ToResponse());
    }

    public async Task<ServiceResult<MuscleResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var muscle = await unitOfWork.Muscles.GetByIdAsync(id, ct);
        if (muscle is null)
            return ServiceResult<MuscleResponse>.NotFound($"Muscle with id '{id}' not found.");

        return ServiceResult<MuscleResponse>.Success(muscle.ToResponse());
    }

    public async Task<ServiceResult<MuscleResponse>> CreateAsync(CreateMuscleRequest request, CancellationToken ct = default)
    {

        var nameExists = await unitOfWork.Muscles.ExistsAsync(p => p.Name == request.Name, ct);
        if (nameExists)
            return ServiceResult<MuscleResponse>.Failure($"A Muscle with name '{request.Name}' already exists.");

        var muscle = Muscle.Create(request.Name);
        await unitOfWork.Muscles.AddAsync(muscle, ct);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult<MuscleResponse>.Success(muscle.ToResponse(), 201);
    }

    public async Task<ServiceResult<MuscleResponse>> UpdateAsync(Guid id, UpdateMuscleRequest request, CancellationToken ct = default)
    {

        var muscle = await unitOfWork.Muscles.GetByIdAsync(id, ct);
        if (muscle is null)
            return ServiceResult<MuscleResponse>.NotFound($"Product with id '{id}' not found.");

        var nameConflict = await unitOfWork.Muscles.ExistsAsync(
            m => m.Name == request.Name && m.Id != id, ct);
        if (nameConflict)
            return ServiceResult<MuscleResponse>.Failure($"A product with name '{request.Name}' already exists.");

        muscle.Update(request.Name);
        unitOfWork.Muscles.Update(muscle);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult<MuscleResponse>.Success(muscle.ToResponse());
    }

    public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var product = await unitOfWork.Muscles.GetByIdAsync(id, ct);
        if (product is null)
            return ServiceResult.NotFound($"Product with id '{id}' not found.");

        unitOfWork.Muscles.Delete(product);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult.Success(204);
    }
}