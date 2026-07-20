using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Shared.VModels;

namespace Shared.VModels
{
    public class InventoriesVM : BaseModelVM
    { 
        public  string OwnerID {  get; set; }
        public  string OwnerType { get; set; }
        public short BonusWeight { get; set; } = 0;
        public short BonusSlots { get; set; } = 0;
        public  string InventoryName { get; set; }
        public  Guid InventoryTypeID { get; set; }

        public  InventoryTypesVM InventoryType { get; set; }

    }
}
