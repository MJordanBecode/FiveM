using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Shared.VModels;

namespace Shared.VModels
{
    public class ItemCategoriesVM : BaseModelVM
    {
       public  string Name { get; set; }
       public string Label { get; set; } = string.Empty;
    }
}
