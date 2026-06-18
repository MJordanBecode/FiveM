using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissions>
{
    public void Configure(EntityTypeBuilder<RolePermissions> builder)
    {
        builder.ToTable("RolePermissions");

        builder.HasKey(x => x.ID);

        builder.HasOne(x => x.Roles)
            .WithMany()
            .HasForeignKey(x => x.RolesID);

        builder.HasOne(x => x.Permissions)
            .WithMany()
            .HasForeignKey(x => x.PermissionsID);

        builder.HasIndex(x => new
        {
            x.RolesID,
            x.PermissionsID
        }).IsUnique();
    }
}