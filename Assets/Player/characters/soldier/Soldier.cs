using System;
using UnityEngine;

namespace Player.characters.soldier{
    public class Soldier : MonoBehaviour, ICharacter{
        public ICharacterData CharacterData{ get; set; }

        private void Awake(){
            CharacterData = new SoldierCharacterData();
        }
    }
}