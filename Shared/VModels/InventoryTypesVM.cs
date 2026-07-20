using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Shared.VModels;

namespace Shared.VModels
{
    public class InventoryTypesVM : BaseModelVM
    {
        public  string Name { get; set; }
        public  int BaseWeight { get; set; }
        public short MaxSlots { get; set; }
    }
}
