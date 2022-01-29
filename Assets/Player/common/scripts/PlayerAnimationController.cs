using Player.Enums;
using UnityEngine;

namespace Player.common.scripts{
    public class PlayerAnimationController : MonoBehaviour{
        private PlayerState currentPlayerState;
        private PlayerModifier currentPlayerModifier;
        private PlayerWeaponType currentPlayerWeaponType;

        public PlayerModifier CurrentPlayerModifier{
            get => currentPlayerModifier;
            set {
                currentPlayerModifier = value;
                _animator.SetInteger(Modifier, (int) value);
            }
        }
        public PlayerWeaponType CurrentPlayerWeaponType{
            get => currentPlayerWeaponType;
            set{
                currentPlayerWeaponType = value;
                _animator.SetInteger(WeaponType, (int) value);
            }
        }
        public PlayerState CurrentPlayerState{
            get => currentPlayerState;
            set{
                currentPlayerState = value;
                _animator.SetInteger(State, (int) value);
            }
        }
        
        private Animator _animator;
        
        // Hashes
        private static readonly int State = Animator.StringToHash("State");
        private static readonly int WeaponType = Animator.StringToHash("WeaponType");
        private static readonly int Modifier = Animator.StringToHash("Modifier");

        private void Start(){
            
            _animator = GetComponent<Animator>();
            
            CurrentPlayerState = PlayerState.Idle;
            CurrentPlayerModifier = PlayerModifier.Normal;
            CurrentPlayerWeaponType = PlayerWeaponType.Rifle;
        }
        
        public void PlayAttackAnimation(){
            CurrentPlayerState = PlayerState.Attacking;
        }
        public void StopAttackAnimation(){
            CurrentPlayerState = PlayerState.Idle;
        }
    }
}
