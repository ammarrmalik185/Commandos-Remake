using System.Collections.Generic;
using UnityEngine;

namespace Managers.InputManager{
    public class InputManager{
        private Dictionary<PlayerActions, KeyCode> keyMapping;

        private static InputManager _instance;

        private InputManager(){
            loadDefault();
        }

        private void loadDefault(){
            keyMapping = new Dictionary<PlayerActions, KeyCode>{
                { PlayerActions.PanForward , KeyCode.W},
                { PlayerActions.PanBackwards , KeyCode.S},
                { PlayerActions.PanLeft , KeyCode.A},
                { PlayerActions.PanRight , KeyCode.D},
                
                { PlayerActions.RotateCameraRight , KeyCode.RightArrow},
                { PlayerActions.RotateCameraLeft , KeyCode.LeftArrow},
                
                { PlayerActions.ToggleProne , KeyCode.Space},
                { PlayerActions.ToggleCrouch , KeyCode.C},

                { PlayerActions.MultiSelect , KeyCode.LeftControl },
                { PlayerActions.Inventory , KeyCode.I }
            };
        }

        public void setKeyMap(PlayerActions action, KeyCode key){
            keyMapping[action] = key;
        }

        public bool GetKey(PlayerActions action){
            return Input.GetKey(keyMapping[action]);
        }
        public bool GetKeyDown(PlayerActions action){
            return Input.GetKeyDown(keyMapping[action]);
        }
        public bool GetKeyUp(PlayerActions action){
            return Input.GetKeyUp(keyMapping[action]);
        }

        public static InputManager getInstance(){
            return _instance ??= new InputManager();
        }
    }
}