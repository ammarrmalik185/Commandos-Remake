using UnityEngine;
using UnityEngine.UI;

namespace Inventory.scripts{
    public interface IInventoryItem{
        GameObject prefab{ get; set; }
        Sprite displayImage{ get; set; }
        string name{ get; set; }
        float range{ get; set; }
        float damage{ get; set; }
        int inventorySize_Height{ get; set; }
        int inventorySize_Width{ get; set; }
        bool isEquipped{ get; set; }
    }
}