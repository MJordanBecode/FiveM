using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class JobGradesVM : BaseModelVM
    {
        public  string Name { get; set; }
        public short Level { get; set; }
        public Guid JobID { get; set; }
        public Guid RoleID { get; set; }

        public  JobsVM Job { get; set; }
        public  RolesVM Role { get; set; }
    }
}
