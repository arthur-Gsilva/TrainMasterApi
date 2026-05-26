using TrainMaster.Application.Common;
using TrainMaster.Application.Interfaces;
using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Application.DTOs;

using TrainMaster.Application.Mappings;


namespace TrainMaster.Application.Services;

public class SubGroupService(
    IUnitOfWork unitOfWork
): ISubGroupService
{
    public async Task<ServiceResult<IEnumerable<SubGroupResponse>>> GetAllAsync(CancellationToken ct = default)
    {
        var subGroups = await unitOfWork.SubGroups.GetAllAsync(ct);
        return ServiceResult<IEnumerable<SubGroupResponse>>.Success(subGroups.ToResponse());
    }

    public async Task<ServiceResult<SubGroupUniqueResponse>> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var subGroup = await unitOfWork.SubGroups.GetByIdWithMuscleAsync(id, ct);
        if (subGroup is null)
            return ServiceResult<SubGroupUniqueResponse>.NotFound($"Muscle with id '{id}' not found.");

        return ServiceResult<SubGroupUniqueResponse>.Success(subGroup.ToUniqueResponse());
    }

    public async Task<ServiceResult<SubGroupResponse>> CreateAsync(CreateSubGroupRequest request, CancellationToken ct = default)
    {

        var nameExists = await unitOfWork.SubGroups.ExistsAsync(p => p.Name == request.Name, ct);
        if (nameExists)
            return ServiceResult<SubGroupResponse>.Failure($"A Muscle with name '{request.Name}' already exists.");

        var muscleExists = await unitOfWork.Muscles.ExistsAsync(m => m.Id == request.MuscleId, ct);
        if (!muscleExists)
            return ServiceResult<SubGroupResponse>.Failure("Muscle not found.", 404);

        var subGroup = SubGroup.Create(request.Name, request.MuscleId);
        await unitOfWork.SubGroups.AddAsync(subGroup, ct);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult<SubGroupResponse>.Success(subGroup.ToResponse(), 201);
    }

    public async Task<ServiceResult<SubGroupResponse>> UpdateAsync(Guid id, UpdateSubGroupRequest request, CancellationToken ct = default)
    {

        var subGroup = await unitOfWork.SubGroups.GetByIdAsync(id, ct);
        if (subGroup is null)
            return ServiceResult<SubGroupResponse>.NotFound($"SubGroup with id '{id}' not found.");

        var nameConflict = await unitOfWork.SubGroups.ExistsAsync(
            m => m.Name == request.Name && m.Id != id, ct);
        if (nameConflict)
            return ServiceResult<SubGroupResponse>.Failure($"A SubGroup with name '{request.Name}' already exists.");

        subGroup.Update(request.Name, request.MuscleId);
        unitOfWork.SubGroups.Update(subGroup);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult<SubGroupResponse>.Success(subGroup.ToResponse());
    }

    public async Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var subGroup = await unitOfWork.SubGroups.GetByIdAsync(id, ct);
        if (subGroup is null)
            return ServiceResult.NotFound($"SubGroup with id '{id}' not found.");

        unitOfWork.SubGroups.Delete(subGroup);
        await unitOfWork.CommitAsync(ct);

        return ServiceResult.Success(204);
    }
}