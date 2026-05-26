using TrainMaster.Domain.Common;
using TrainMaster.Domain.Enums;

namespace TrainMaster.Domain.Entities;

public class Workout : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid MuscleId { get; private set; }
    public Muscle Muscle { get; private set; } = null!;
    public Guid SubGroupId { get; private set; }
    public SubGroup SubGroup { get; private set; } = null!;
    public WorkoutTypes Type { get; private set; } = WorkoutTypes.bodybuilding;
    public string? Url_video { get; private set; }
    public string? Url_image { get; private set; }
    public WorkoutLevels Level { get; private set; } = WorkoutLevels.beginner;
    public ICollection<TrainingWorkout> TrainingWorkout {get; private set;} = [];


    private Workout() {}

    public static Workout Create(string name, string description, Guid muscleId, Guid subGroupId, WorkoutTypes type, string? url_video, string? url_image, WorkoutLevels level)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(muscleId.ToString());
        ArgumentException.ThrowIfNullOrWhiteSpace(subGroupId.ToString());

        return new Workout
        {
            Name = name,
            Description = description,
            MuscleId = muscleId,
            SubGroupId = subGroupId,
            Type = type,
            Url_video = url_video,
            Url_image = url_image,
            Level = level
        };
    }

    public void Update(string name, string description, Guid muscleId, Guid subGroupId, WorkoutTypes type, string? url_video, string? url_image, WorkoutLevels level)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(muscleId.ToString());
        ArgumentException.ThrowIfNullOrWhiteSpace(subGroupId.ToString());

        Name = name;
        Description = description;
        MuscleId = muscleId;
        SubGroupId = subGroupId;
        Type = type;
        Url_video = url_video;
        Url_image = url_image;
        Level = level;
        SetUpdatedAt();
    }
}