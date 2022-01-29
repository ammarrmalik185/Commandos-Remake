using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.scripts{
    public enum InventorySide{
        Right,
        Left
    }
    
    internal class InventoryGrid{
        private readonly InventorySlot[][] grid;
        private static readonly Dictionary<InventorySide,bool> isAttachedToUI = new Dictionary<InventorySide, bool>();
        private static readonly Dictionary<InventorySide,InventoryGrid> attachedInventory = new Dictionary<InventorySide, InventoryGrid>();

        public Player.common.scripts.Player belongsTo;

        public InventoryGrid(int height, int width){
            grid = new InventorySlot[height][];
            for (var x = 0; x < height; x++){
                grid[x] = new InventorySlot[width];
                for (var y = 0; y < width; y++){
                    var slot = new InventorySlot();
                    grid[x][y] = slot;
                    slot.indexX = x;
                    slot.indexY = y;
                }
            }
        }

        public void PutInventoryToUI(InventorySide side){
            if (isAttachedToUI.ContainsKey(side) && isAttachedToUI[side])
                attachedInventory[side].RemoveInventoryFromUI();
            isAttachedToUI[side] = true;
            attachedInventory[side] = this;
            var gridGameObject = side switch{
                InventorySide.Right => UIManager.getInstance().rightInventoryGrid,
                InventorySide.Left => UIManager.getInstance().leftInventoryGrid,
                _ => UIManager.getInstance().inventoryGrid
            };

            gridGameObject
                .GetComponentInChildren<InventorySubtitleTag>(true).Start();
            gridGameObject
                .GetComponentInChildren<InventorySubtitleTag>(true)
                .SetSubTitle(belongsTo.characterData.name);

            var gridRows = gridGameObject.GetComponentsInChildren<HorizontalLayoutGroup>(true);
            for (var x = 0; x < grid.Length; x++){
                var currentColumn = gridRows[x];
                var columnRows = currentColumn.gameObject.GetComponentsInChildren<InventoryCell>();
                for (var y = 0; y < grid[x].Length; y++){
                    columnRows[y].Start();
                    columnRows[y].Slot = grid[x][y];
                    grid[x][y].itemCell = columnRows[y];
                }
            }
        }

        private void RemoveInventoryFromUI(){
            foreach (var t in grid){
                foreach (var t1 in t){
                    t1.itemCell = null;
                }
            }
        }

        public InventorySlot getSlot(int indexX, int indexY){
            return grid[indexX][indexY];
        }
        
    }
}