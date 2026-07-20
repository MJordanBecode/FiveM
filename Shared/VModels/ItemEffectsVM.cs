using Shared.VModels;
using System;
using System.ComponentModel.DataAnnotations;


namespace Shared.VModels
{
    public class ItemEffectsVM : BaseModelVM
    {
        public  string EffectType { get; set; }    // ex: heal, armor, hunger, thirst
        public short ValueType { get; set; }    // ex: percentage, fixed
        public Guid ItemID { get; set; }

        public  ItemsVM Item { get; set; }

    }
}
