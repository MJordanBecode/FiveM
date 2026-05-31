using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class RolePermissions : BaseModel
    {
        public Guid RolesID { get; set; }
        public Guid PermissionsID { get; set; }

        public required Roles Roles { get; set; }
        public required Permissions Permissions { get; set; }
    }
}
