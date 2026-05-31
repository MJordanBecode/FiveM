using FivemCsharpCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class RolePlayers : BaseModel
    {
        public Guid RoleID { get; set; }
        public Guid PlayerID { get; set; }

        public required Roles Role { get; set; }
        public required Players Player { get; set; }
    }
}
