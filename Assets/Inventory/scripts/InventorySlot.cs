namespace Inventory.scripts{
    public class InventorySlot{
        public bool isFree{ get; set; }
        private IInventoryItem itemRef;
        public IInventoryItem ItemRef{ 
            get => itemRef;
            set{
                itemRef = value;
                if (itemCell != null)
                    itemCell.ItemUpdated();
                isFree = itemRef == null;
            }
        }
        public InventoryCell itemCell{ get; set; }
        public bool isEquipped{ get; set; }
        public int indexX{ get; set; }
        public int indexY{ get; set; }

        public InventorySlot(){
            ItemRef = null;
        }
        
    }
}