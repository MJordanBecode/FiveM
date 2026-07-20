using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shared.VModels;

namespace Shared.VModels
{
    public class PlayerSkinsVM : BaseModelVM
    {

        public  string Face { get; set; }
        public  string Hair { get; set; }
        public  string Clothes { get; set; } //check pour voir si ce n'est pas mieux de mettre un json ici 
        public string? Props { get; set; }
        public string? Overlays { get; set; }


    }
}
