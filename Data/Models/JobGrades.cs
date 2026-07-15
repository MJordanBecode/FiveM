using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class JobGrades : BaseModel
    {
        public  string Name { get; set; }
        public short Level { get; set; }
        public Guid JobID { get; set; }
        public Guid RoleID { get; set; }

        public  Jobs Job { get; set; }
        public  Roles Role { get; set; }
    }
}
