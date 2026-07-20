using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Garages : BaseModel
    {
        public  string Name { get; set; }
        public  string OwnerType { get; set; }
        public  string OwnerID { get; set; }
        public  string Position { get; set; } //format json {x: 0.0, y: 0.0, z: 0.0}
        public Guid GarageCategoryID { get; set; }
        public  GarageCategories GarageCategory { get; set; }
    }
}
