using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class RolePlayers : BaseModel
    {
        public Guid RoleID { get; set; }
        public Guid PlayerID { get; set; }

        public  Roles Role { get; set; }
        public  Players Player { get; set; }
    }
}
