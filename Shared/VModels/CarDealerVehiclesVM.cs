using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class CarDealerVehiclesVM : BaseModelVM
    {
        public long Price { get; set; }
        public short Stock { get; set; } = 0;
        public Guid VehicleID { get; set; }
        public Guid CarDealerID { get; set; }
        public Guid GarageID { get; set; }

        public required VehiclesVM Vehicle { get; set; }
        public required CarDealersVM CarDealer { get; set; }
        public required GaragesVM Garage { get; set; }
    }
}
