using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.VModels;

namespace Shared.VModels
{
    public class RolePermissionsVM : BaseModelVM
    {
        public Guid RolesID { get; set; }
        public Guid PermissionsID { get; set; }

        public  RolesVM Roles { get; set; }
        public  PermissionsVM Permissions { get; set; }
    }
}
