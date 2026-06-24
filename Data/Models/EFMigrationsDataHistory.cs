using System;

public class EFMigrationsDataHistory
{
    public int Id { get; set; }

    public string MigrationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}