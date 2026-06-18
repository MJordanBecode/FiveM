using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class JobGradeConfiguration : IEntityTypeConfiguration<JobGrades>
{
    public void Configure(EntityTypeBuilder<JobGrades> builder)
    {
        builder.ToTable("JobGrades");

        builder.HasKey(jg => jg.ID);

        builder.Property(jg => jg.Name)
            .IsRequired()
            .HasMaxLength(30);

        builder.HasOne(jg => jg.Job)
            .WithMany()
            .HasForeignKey(jg => jg.JobID);

        builder.HasOne(jg => jg.Role)
            .WithMany()
            .HasForeignKey(jg => jg.RoleID);

        builder.HasIndex(jg => new { jg.JobID, jg.Level }).IsUnique();
    }
}