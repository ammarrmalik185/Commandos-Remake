using UnityEngine;

namespace Player.characters.doctor{
    public class Doctor : MonoBehaviour, ICharacter{
        public ICharacterData CharacterData{ get; set; }

        private void Awake(){
            CharacterData = new DoctorCharacterData();
        }
    }
}