using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shared.VModels;

namespace Shared.VModels
{
    public class PlayerCharactersVM : BaseModelVM
    {
        public Guid PlayerID { get; set; }


        // Identité RP
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public DateTime? BirthDay { get; set; }

        public short? Height { get; set; }

        public char? Gender { get; set; }


        // Apparence
        public Guid? SkinID { get; set; }


        // Relations

        public PlayersVM Player { get; set; }

        public PlayerSkinsVM? Skin { get; set; }

    }
}
