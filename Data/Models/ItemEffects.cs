using Data.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace Data.Models
{
    public class ItemEffects : BaseModel
    {
        public  string EffectType { get; set; }    // ex: heal, armor, hunger, thirst
        public short ValueType { get; set; }    // ex: percentage, fixed
        public Guid ItemID { get; set; }

        public  Items Item { get; set; }

    }
}
