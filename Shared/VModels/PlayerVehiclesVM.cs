using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.VModels;

namespace Shared.VModels
{
    public class PlayerVehiclesVM : BaseModelVM
    {
        public  string Plate { get; set; }
        public short Fuel { get; set; }
        public short EngineHealth { get; set; }
        public short BodyHealth { get; set; }
        public short State { get; set; } //Revoir à quoi il sert exactement
        public string? VehicleProps { get; set; } //Permet de stocker les customs d'un véhicule

        public Guid VehicleID { get; set; }
        public Guid PlayerID { get; set; }

        public  VehiclesVM Vehicle { get; set; }
        public  PlayersVM Player { get; set; }

    }
}
