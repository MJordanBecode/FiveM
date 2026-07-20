using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.VModels;

namespace Shared.VModels
{
    public class GarageCategoriesVM : BaseModelVM
    {
        public  string Name { get; set; }
        public short MaxSlots { get; set; } = 1;
        public short MinSlots { get; set; } = 0;
        public long Price { get; set; } 

        public  string VehicleType { get; set; } //format json ["car", "boat", "aircraft"]

    }
}
