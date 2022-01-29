using UnityEngine;
using UnityEngine.UI;

namespace Player.characters.soldier{
    public class SoldierCharacterData : ICharacterData{
        public GameObject prefab{ get; set; }
        public Sprite displayImage{ get; set; }
        public string name{ get; set; }

        internal SoldierCharacterData(){
            // prefab = Resources.Load<GameObject>("characters/soldier/prefab");
            displayImage = Resources.Load<Sprite>("characters/soldier/displayImage");
            name = "Soldier";
        }
    }
}