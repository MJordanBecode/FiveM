using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class Roles : BaseModel
    {
        public required string RoleName { get; set; }
        public required string Label { get; set; }
    }
}
