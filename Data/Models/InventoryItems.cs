using FivemCsharpCore.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace ClassLibrary1.Models
{
    public class InventoryItems : BaseModel
    {
        public short Quantity { get; set; }
        public byte Durability { get; set; } = 100;
        public required string Metadata { get; set; }
        public short Slots  { get; set; }

        public Guid PlayerID { get; set; }
        public Guid InventoryID { get; set; }
        public Guid ItemID { get; set; }

        public required Players Player { get; set; }
        public required Inventories Inventory { get; set; }
        public required Items Item { get; set; }
    }
}
