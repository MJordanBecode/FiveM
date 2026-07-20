using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.VModels;

namespace Shared.VModels
{
    public class CarDealerVehiclesVM : BaseModelVM
    {
        public long Price { get; set; }
        public short Stock { get; set; } = 0;
        public Guid VehicleID { get; set; }
        public Guid CarDealerID { get; set; }
        public Guid GarageID { get; set; }

        public  VehiclesVM Vehicle { get; set; }
        public  CarDealersVM CarDealer { get; set; }
        public  GaragesVM Garage { get; set; }
    }
}
