using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class PlayerSkins : BaseModel
    {

        public  string Face { get; set; }
        public  string Hair { get; set; }
        public  string Clothes { get; set; }
        public string? Props { get; set; }
        public string? Overlays { get; set; }


    }
}
