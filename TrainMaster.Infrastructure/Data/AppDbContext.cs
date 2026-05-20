using TrainMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TrainMaster.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Muscle> Muscles => Set<Muscle>();
    public DbSet<SubGroup> SubGroups => Set<SubGroup>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<Training> Trainings => Set<Training>();
    public DbSet<TrainingWorkout> TrainingWorkouts => Set<TrainingWorkout>();
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
