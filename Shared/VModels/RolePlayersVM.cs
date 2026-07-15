using FivemCsharpCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class RolePlayersVM : BaseModelVM
    {
        public Guid RoleID { get; set; }
        public Guid PlayerID { get; set; }

        public  RolesVM Role { get; set; }
        public  PlayersVM Player { get; set; }
    }
}
