using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Models
{
    public class PlayerCharactersVM : BaseModelVM
    {    
     public Guid PlayerID { get; set; }
     public Guid PlayerSkinID { get; set; }
     public required PlayerSkinsVM Player { get; set; }
     public required PlayerSkinsVM PlayerSkin { get; set; }

    }
}
