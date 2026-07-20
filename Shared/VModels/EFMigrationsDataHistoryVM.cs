using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.VModels
{
    public class EFMigrationsDataHistoryVM
    {
        public int Id { get; set; }

        public string MigrationId { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
