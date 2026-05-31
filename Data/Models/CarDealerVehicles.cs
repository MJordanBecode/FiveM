 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class CarDealerVehicles : BaseModel
    {
        public long Price { get; set; }
        public short Stock { get; set; } = 0;
        public Guid VehicleID { get; set; }
        public Guid CarDealerID { get; set; }
        public Guid GarageID { get; set; }

        public required Vehicles Vehicle { get; set; }
        public required CarDealers CarDealer { get; set; }
        public required Garages Garage { get; set; }
    }
}
