using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Models
{
    public class ItemsVM : BaseModelVM
    {
        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(30)]
        public required string Label { get; set; } = string.Empty;
        [Required]
        public required short Weight { get; set; }
        [Required]
        public bool Stackable { get; set; }
        [Required]
        public short MaxStack { get; set; }
        [Required]
        public short MinStack { get; set; } = 1;
        public string Description { get; set; } = string.Empty;

        public Guid CategoryID { get; set; }
        public required ItemCategoriesVM Category { get; set; }


    }
}
