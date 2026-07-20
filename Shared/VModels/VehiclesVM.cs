using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shared.VModels;

namespace Shared.VModels
{
    public class VehiclesVM : BaseModelVM
    {
        public  string Name { get; set; }
        public  string SpawnName { get; set; }
        public long BasePrice { get; set; } = 0;
        public Guid CategoryID { get; set; }

        public  VehiclecategoriesVM Category { get; set; }

    }
}
