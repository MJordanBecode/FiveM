using FivemCsharpCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClassLibrary1.Models
{
    public class Vehicles : BaseModel
    {
        public  string Name { get; set; }
        public  string SpawnName { get; set; }
        public long BasePrice { get; set; } = 0;
        public Guid CategoryID { get; set; }

        public  Vehiclecategories Category { get; set; }

    }
}
