using UnityEngine;

namespace Player.characters.doctor{
    public class DoctorCharacterData : ICharacterData{
        public GameObject prefab{ get; set; }
        public Sprite displayImage{ get; set; }
        public string name{ get; set; }

        internal DoctorCharacterData(){
            // prefab = Resources.Load<GameObject>("characters/soldier/prefab");
            displayImage = Resources.Load<Sprite>("characters/doctor/displayImage");
            name = "Doctor";
        }
    }
}