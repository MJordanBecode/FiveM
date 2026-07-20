using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.VModels;

namespace Shared.VModels
{
    public class GaragesVM : BaseModelVM
    {
        public  string Name { get; set; }
        public  string OwnerType { get; set; }
        public  string OwnerID { get; set; }
        public  string Position { get; set; } //format json {x: 0.0, y: 0.0, z: 0.0}
        public Guid GarageCategoryID { get; set; }
        public  GarageCategoriesVM GarageCategory { get; set; }
    }
}
