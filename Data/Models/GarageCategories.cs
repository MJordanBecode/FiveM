using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class GarageCategories : BaseModel
    {
        public required string Name { get; set; }
        public short MaxSlots { get; set; } = 1;
        public short MinSlots { get; set; } = 0;
        public long Price { get; set; } 

        public required string VehicleType { get; set; } //format json ["car", "boat", "aircraft"]

    }
}
