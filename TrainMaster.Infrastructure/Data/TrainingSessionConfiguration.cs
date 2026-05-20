using TrainMaster.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TrainMaster.Infrastructure.Data;

public class TrainingSessionConfiguration : IEntityTypeConfiguration<TrainingSession>
{
    public void Configure(EntityTypeBuilder<TrainingSession> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Date)
            .IsRequired();

        builder.Property(t => t.Duration);

        builder.HasOne(t => t.User)
            .WithMany(t => t.TrainingSession)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Training)
            .WithMany(t => t.TrainingSession)
            .HasForeignKey(t => t.TrainingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(t => t.CreatedAt)
            .IsRequired();

        builder.Property(t => t.UpdatedAt);
    }
}