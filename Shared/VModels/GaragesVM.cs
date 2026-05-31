using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class GaragesVM : BaseModelVM
    {
        public required string Name { get; set; }
        public required string OwnerType { get; set; }
        public required string OwnerID { get; set; }
        public required string Position { get; set; } //format json {x: 0.0, y: 0.0, z: 0.0}
        public Guid GarageCategoryID { get; set; }
        public required GarageCategoriesVM GarageCategory { get; set; }
    }
}
