using UI;
using UnityEngine;

namespace Inventory.scripts{
    public class Inventory{
        private const int height = 6;
        private const int width = 5;

        private readonly InventoryGrid grid;
        private InventorySide openSide;
        public Inventory(Player.common.scripts.Player owner){
            grid = new InventoryGrid(height,width){
                belongsTo = owner
            };
        }

        public void showInventoryOnLeft(){
            UIManager.getInstance().leftInventoryGrid.gameObject.SetActive(true);
            grid.PutInventoryToUI(InventorySide.Left);
            openSide = InventorySide.Left;
        }
        
        public void showInventoryOnRight(){
            UIManager.getInstance().rightInventoryGrid.gameObject.SetActive(true);
            grid.PutInventoryToUI(InventorySide.Right);
            openSide = InventorySide.Right;
        }

        public void closeInventory(){
            switch (openSide){
                case InventorySide.Left:
                    UIManager.getInstance().leftInventoryGrid.gameObject.SetActive(false);
                    break;
                case InventorySide.Right:
                    UIManager.getInstance().rightInventoryGrid.gameObject.SetActive(false);
                    break;
                default:
                    Debug.Log("inventory is not open");
                    break;
            }
        }
        
        public void PutItemInInventory(int index1, int index2, IInventoryItem item){
            if (index1 < width && index2 < height)
                grid.getSlot(index1, index2).ItemRef = item;
        }

    }
}