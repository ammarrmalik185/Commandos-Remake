using Inventory.scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.items.basicRifle{
    internal class basicRifle : IInventoryItem{
        public GameObject prefab{ get; set; }
        public Sprite displayImage{ get; set; }
        public string name{ get; set; }
        public float range{ get; set; }
        public float damage{ get; set; }
        public int inventorySize_Height{ get; set; }
        public int inventorySize_Width{ get; set; }
        public bool isEquipped{ get; set; }

        internal basicRifle(){
            prefab = Resources.Load<GameObject>("/items/basicRifle/prefab");
        }
    }
}