using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Models
{
    public class PlayerCharacters : BaseModel
    {    
     public Guid PlayerID { get; set; }
     public Guid PlayerSkinID { get; set; }
     public required PlayerSkins Player { get; set; }
     public required PlayerSkins PlayerSkin { get; set; }

    }
}
