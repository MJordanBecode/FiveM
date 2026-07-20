using Data.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace Data.Models
{
    public class InventoryItems : BaseModel
    {
        public short Quantity { get; set; }
        public byte Durability { get; set; } = 100;
        public  string Metadata { get; set; }
        public short Slots  { get; set; }

        public Guid PlayerID { get; set; }
        public Guid InventoryID { get; set; }
        public Guid ItemID { get; set; }

        public  Players Player { get; set; }
        public  Inventories Inventory { get; set; }
        public  Items Item { get; set; }
    }
}
