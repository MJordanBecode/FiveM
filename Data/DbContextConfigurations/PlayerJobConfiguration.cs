using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class PlayerJobConfiguration : IEntityTypeConfiguration<PlayerJobs>
{
    public void Configure(EntityTypeBuilder<PlayerJobs> builder)
    {
        builder.ToTable("PlayerJobs");

        builder.HasKey(x => x.ID);

        builder.HasOne(x => x.Player)
            .WithMany()
            .HasForeignKey(x => x.PlayerID);

        builder.HasOne(x => x.Job)
            .WithMany()
            .HasForeignKey(x => x.JobID);

        builder.HasOne(x => x.JobGrade)
            .WithMany()
            .HasForeignKey(x => x.JobGradeID);

        builder.HasIndex(x => new
        {
            x.PlayerID,
            x.JobID
        }).IsUnique();
    }
}