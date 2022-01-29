using UnityEngine;
using UnityEngine.UI;

namespace Inventory.scripts{
    public class InventoryCell : MonoBehaviour{
        private InventorySlot slot;
        private ItemCellDragableImage itemCellDragableImage;
        internal static Sprite defaultSprite;
        
        public InventorySlot Slot{
            get => slot;
            set{
                slot = value;
                if (slot != null){
                    ItemUpdated();
                }
            }
        }

        private bool started;
        
        public void Start(){
            if (!started){
                defaultSprite = Resources.Load<Sprite>("items/defaults/displayImage");
                var itemCellImage = GetComponentInChildren<ItemCellDragableImage>(true);
                itemCellImage.Start();
                itemCellDragableImage = itemCellImage;
                itemCellImage.inventoryCell = this;
                started = true;
            }
        }

        public void ItemUpdated(){
            itemCellDragableImage.image.sprite = slot.ItemRef != null ? slot.ItemRef.displayImage : defaultSprite;
        }
    }
}
