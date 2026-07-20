using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Items : BaseModel
    {
        [Required]
        [MaxLength(30)]
        public  string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public  string Label { get; set; } = string.Empty;
        [Required]
        public  short Weight { get; set; }
        [Required]
        public bool Stackable { get; set; }
        [Required]
        public short MaxStack { get; set; }
        [Required]
        public short MinStack { get; set; } = 1;
        public string Description { get; set; } = string.Empty;

        public Guid CategoryID { get; set; }
        public  ItemCategories Category { get; set; }


    }
}
