using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class CarDealers : BaseModel
    {
        public  string Name { get; set; }
        public  string Location { get; set; } //format json {x: 0.0, y: 0.0, z: 0.0}
    }
}
