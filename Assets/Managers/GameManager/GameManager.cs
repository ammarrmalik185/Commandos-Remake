using System;
using UnityEngine;

namespace Managers.GameManager{
    public class GameManager : MonoBehaviour{
        private static GameManager _instance;

        private GameState state;
        public GameState State{
            get => state;
            set{
                state = value;
                switch (state){
                    case GameState.Paused:
                        canEnemiesMove = false;
                        canPlayerMove = false;
                        canSelectionBoxWork = false;
                        isMouseEnabled = true;
                        canCameraMove = false;
                        break;
                    case GameState.MenuOpen:
                        canEnemiesMove = false;
                        canPlayerMove = false;
                        canSelectionBoxWork = false;
                        isMouseEnabled = true;
                        canCameraMove = false;
                        break;
                    case GameState.Normal:
                        canEnemiesMove = true;
                        canPlayerMove = true;
                        canSelectionBoxWork = true;
                        isMouseEnabled = true;
                        canCameraMove = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        [HideInInspector] public bool canPlayerMove;
        [HideInInspector] public bool canEnemiesMove;
        [HideInInspector] public bool canSelectionBoxWork;
        [HideInInspector] public bool isMouseEnabled;
        [HideInInspector] public bool canCameraMove;

        private void Awake(){
            State = GameState.Normal;
            _instance = this;
        }

        public void PauseGame(){
            State = GameState.Paused;
        }

        public void UnpauseGame(){
            State = GameState.Normal;
        }

        public void MenuIsOpen(){
            State = GameState.MenuOpen;
        }

        public void MenuIsClosed(){
            State = GameState.Normal;
        }

        public static GameManager getInstance(){
            return _instance;
        }

    }
}