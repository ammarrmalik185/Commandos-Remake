using Inventory.scripts;
using UnityEngine;

namespace Inventory.items.basicPistol{
    internal class basicPistol : IInventoryItem{
        public GameObject prefab{ get; set; }
        public Sprite displayImage{ get; set; }
        public string name{ get; set; }
        public float range{ get; set; }
        public float damage{ get; set; }
        public int inventorySize_Height{ get; set; }
        public int inventorySize_Width{ get; set; }
        public bool isEquipped{ get; set; }

        internal basicPistol(){
            displayImage = Resources.Load<Sprite>("items/basicPistol/displayImage");
        }
    }
}