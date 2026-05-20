using TrainMaster.Domain.Entities;

namespace TrainMaster.Domain.Interfaces;

public interface ITrainingRepository : IRepository<Training> {}
public interface ITrainingWorkoutRepository : IRepository<TrainingWorkout> {}
public interface ITrainingSessionRepository : IRepository<TrainingSession> {}