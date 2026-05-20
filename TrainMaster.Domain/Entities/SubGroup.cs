using TrainMaster.Domain.Common;


namespace TrainMaster.Domain.Entities;

public class SubGroup : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public Guid MuscleId { get; private set; }
    public Muscle Muscle { get; private set; } = null!;
    public ICollection<Workout> Workouts { get; private set; } = new List<Workout>();


    private SubGroup() {}

    public static SubGroup Create(string name, Guid muscleId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(muscleId.ToString());

        return new SubGroup
        {
            Name = name,
            MuscleId = muscleId
        };
    }

    public void Update(string name, Guid muscleId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        MuscleId = muscleId;
        SetUpdatedAt();
    }
}