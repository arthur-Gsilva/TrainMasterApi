using TrainMaster.Domain.Common;

namespace TrainMaster.Domain.Entities;

public class Training : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public ICollection<TrainingWorkout> TrainingWorkout {get; private set;} = [];
    public ICollection<TrainingSession> TrainingSession {get; private set;} = [];

    private Training() {}

    public static Training Create(string name, string description, Guid userId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(userId.ToString());

        return new Training
        {
            Name = name,
            Description = description,
            UserId = userId
        };
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        SetUpdatedAt();
    }
}

public class TrainingWorkout : BaseEntity
{
    public int Order {get; private set;}
    public int Series {get; private set;}
    public int Reps {get; private set;}
    public int Weight {get; private set;}
    public Guid TrainingId {get; private set;}
    public Training Training {get; private set;} = null!;
    public Guid WorkoutId {get; private set;}
    public Workout Workout {get; private set;} = null!;

    private TrainingWorkout (){}

    public static TrainingWorkout Create(int order, int series, int reps, int weight, Guid trainingId, Guid workoutId)
    {

        ArgumentException.ThrowIfNullOrWhiteSpace(trainingId.ToString());
        ArgumentException.ThrowIfNullOrWhiteSpace(workoutId.ToString());

        return new TrainingWorkout
        {
            Order = order,
            Series = series,
            Reps = reps,
            Weight = weight,
            TrainingId = trainingId,
            WorkoutId = workoutId,
        };
    }

    public void Update(int order, int series, int reps, int weight, Guid workoutId)
    {
        Order = order;
        Series = series;
        Reps = reps;
        Weight = weight;
        WorkoutId = workoutId;
        SetUpdatedAt();
    }
}

public class TrainingSession : BaseEntity
{
    public DateTime Date {get; private set;}
    public int Duration {get; private set;}
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;
    public Guid TrainingId {get; private set;}
    public Training Training {get; private set;} = null!;

    private TrainingSession() {}

    public static TrainingSession Create(DateTime date, int duration, Guid userId, Guid trainingId)
    {

        ArgumentException.ThrowIfNullOrWhiteSpace(userId.ToString());
        ArgumentException.ThrowIfNullOrWhiteSpace(trainingId.ToString());

        return new TrainingSession
        {
            Date = date,
            Duration = duration,
            UserId = userId,
            TrainingId = trainingId  
        };
    }

    public void Update(DateTime date, int duration, Guid trainingId)
    {
        Date = date;
        Duration = duration;
        TrainingId = trainingId;
        SetUpdatedAt();
    }
}