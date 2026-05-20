using TrainMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainMaster.Infrastructure.Data;

public class TrainingWorkoutConfiguration : IEntityTypeConfiguration<TrainingWorkout>
{
    public void Configure(EntityTypeBuilder<TrainingWorkout> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Order)
            .IsRequired();

        builder.Property(t => t.Series)
            .IsRequired();
            
        builder.Property(t => t.Reps)
            .IsRequired();

        builder.Property(t => t.Weight);

        builder.HasOne(t => t.Training)
            .WithMany(t => t.TrainingWorkout)
            .HasForeignKey(t => t.TrainingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Workout)
            .WithMany(t => t.TrainingWorkout)
            .HasForeignKey(t => t.WorkoutId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.UpdatedAt);
    }
}