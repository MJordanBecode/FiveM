using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class Inventories : BaseModel
    { 
        public required string OwnerID {  get; set; }
        public required string OwnerType { get; set; }
        public short BonusWeight { get; set; } = 0;
        public short BonusSlots { get; set; } = 0;
        public required string InventoryName { get; set; }
        public required Guid InventoryTypeID { get; set; }

        public required InventoryTypes InventoryType { get; set; }

    }
}
