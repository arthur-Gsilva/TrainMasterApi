using TrainMaster.Domain.Entities;
using TrainMaster.Domain.Interfaces;
using TrainMaster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TrainMaster.Infrastructure.Repositories;

public class TrainingRepository(AppDbContext context)
    : Repository<Training>(context), ITrainingRepository {}
public class TrainingWorkoutRepository(AppDbContext context)
    : Repository<TrainingWorkout>(context), ITrainingWorkoutRepository {}
public class TrainingSessionRepository(AppDbContext context)
    : Repository<TrainingSession>(context), ITrainingSessionRepository {}

