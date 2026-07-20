using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ItemCategories : BaseModel
    {
       public  string Name { get; set; }
       public string Label { get; set; } = string.Empty;
    }
}
