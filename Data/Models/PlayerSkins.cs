using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Models
{
    public class PlayerSkins : BaseModel
    {

        public required string Face { get; set; }
        public required string Hair { get; set; }
        public required string Clothes { get; set; }
        public string? Props { get; set; }
        public string? Overlays { get; set; }


    }
}
