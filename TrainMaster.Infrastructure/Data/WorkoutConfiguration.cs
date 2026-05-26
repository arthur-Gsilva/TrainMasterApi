using TrainMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainMaster.Infrastructure.Data;

public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(w => w.Name).IsUnique();

        builder.Property(w => w.Description)
            .HasMaxLength(500);

        builder.HasOne(w => w.Muscle)
            .WithMany(m => m.Workouts)
            .HasForeignKey(w => w.MuscleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(w => w.SubGroup)
            .WithMany(sg => sg.Workouts)
            .HasForeignKey(w => w.SubGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(w => w.Type)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(w => w.Level)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(w => w.Url_video)
            .HasMaxLength(200).IsRequired(false);

        builder.Property(w => w.Url_image)
            .HasMaxLength(200).IsRequired(false);
    }
}