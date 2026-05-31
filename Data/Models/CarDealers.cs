using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class CarDealers : BaseModel
    {
        public required string Name { get; set; }
        public required string Location { get; set; } //format json {x: 0.0, y: 0.0, z: 0.0}
    }
}
