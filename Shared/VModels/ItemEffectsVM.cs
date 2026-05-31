using FivemCsharpCore.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace ClassLibrary1.Models
{
    public class ItemEffectsVM : BaseModelVM
    {
        public required string EffectType { get; set; }    // ex: heal, armor, hunger, thirst
        public short ValueType { get; set; }    // ex: percentage, fixed
        public Guid ItemID { get; set; }

        public required ItemsVM Item { get; set; }

    }
}
