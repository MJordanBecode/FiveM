using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class JobGradesVM : BaseModelVM
    {
        public required string Name { get; set; }
        public short Level { get; set; }
        public Guid JobID { get; set; }
        public Guid RoleID { get; set; }

        public required JobsVM Job { get; set; }
        public required RolesVM Role { get; set; }
    }
}
