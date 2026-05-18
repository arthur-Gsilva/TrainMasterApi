using TrainMaster.Application.Common;
using TrainMaster.Application.DTOs;

namespace TrainMaster.Application.Interfaces;

public interface ISubGroupService
{
    Task<ServiceResult<IEnumerable<SubGroupResponse>>> GetAllAsync(CancellationToken ct = default);
    Task<ServiceResult<SubGroupUniqueResponse>> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<ServiceResult<SubGroupResponse>> CreateAsync(CreateSubGroupRequest request, CancellationToken ct = default);
    Task<ServiceResult<SubGroupResponse>> UpdateAsync(Guid id, UpdateSubGroupRequest request, CancellationToken ct = default);
    Task<ServiceResult> DeleteAsync(Guid id, CancellationToken ct = default);
}