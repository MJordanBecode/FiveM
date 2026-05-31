using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class JobsVM : BaseModelVM
    {
        public required string Name { get; set; }
        public required string Logo { get; set; }
    }
}
