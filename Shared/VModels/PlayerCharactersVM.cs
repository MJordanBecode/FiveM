using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shared.VModels;

namespace Shared.VModels
{
    public class PlayerCharactersVM : BaseModelVM
    {    
     public Guid PlayerID { get; set; }
     public Guid PlayerSkinID { get; set; }
     public  PlayerSkinsVM Player { get; set; }
     public  PlayerSkinsVM PlayerSkin { get; set; }

    }
}
