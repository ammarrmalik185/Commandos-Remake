using UnityEngine;
using UnityEngine.UI;

namespace Player.characters{
    public interface ICharacterData{
        GameObject prefab{ get; set; }
        Sprite displayImage{ get; set; }
        string name{ get; set; }
    }
}