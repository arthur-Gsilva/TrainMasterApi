using TrainMaster.Domain.Common;


namespace TrainMaster.Domain.Entities;

public class Workout : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string MuscleGroup { get; private set; } = string.Empty;
}