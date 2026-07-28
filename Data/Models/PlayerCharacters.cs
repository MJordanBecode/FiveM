using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Data.Models
{
    public class PlayerCharacters : BaseModel
    {


        // Identité RP
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime? BirthDay { get; set; }

        public short? Height { get; set; }

        public char? Gender { get; set; }


        // Apparence
        public Guid PlayerID { get; set; }
        public Guid? SkinID { get; set; }


        // 🟢 Position (dernière déconnexion)
        public float? PositionX { get; set; }
        public float? PositionY { get; set; }
        public float? PositionZ { get; set; }
        public float? Heading { get; set; }

        // Relations

        public Players Player { get; set; }

        public PlayerSkins? Skin { get; set; }

    }
}
