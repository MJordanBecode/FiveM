using Shared.VModels;
using System;
using System.ComponentModel.DataAnnotations;


namespace Shared.VModels
{
    public class InventoryItemsVM : BaseModelVM
    {
        public short Quantity { get; set; }
        public byte Durability { get; set; } = 100;
        public  string Metadata { get; set; }
        public short Slots  { get; set; }

        public Guid PlayerID { get; set; }
        public Guid InventoryID { get; set; }
        public Guid ItemID { get; set; }

        public  PlayersVM Player { get; set; }
        public  InventoriesVM Inventory { get; set; }
        public  ItemsVM Item { get; set; }

    }
}
