using System.Collections.Generic;
using System.Linq;
using Managers.GameManager;
using Managers.InputManager;
using Player.characters;
using Player.Enums;
using UI;
using UnityEngine;

namespace Player.common.scripts{
    public class PlayerManager : MonoBehaviour
    {
        // Start is called before the first frame update
        private readonly InputManager inputManager = InputManager.getInstance();
        private List<Transform> selectedAgentTransforms;
        private Camera _mainCamera;
        private Ray _ray;
        private RaycastHit _hit;

        private bool inventoryIsOpen;
        private List<Inventory.scripts.Inventory> openInventories;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        private void Update(){
            
            if (InputManager.getInstance().GetKeyDown(PlayerActions.ToggleCrouch)){
                if (selectedAgentTransforms == null || selectedAgentTransforms.Count == 0) return;
                foreach (var controller in selectedAgentTransforms.Select(selectedAgentTransform => selectedAgentTransform.GetComponent<PlayerAnimationController>())){
                    controller.CurrentPlayerModifier = controller.CurrentPlayerModifier != PlayerModifier.Crouch ? PlayerModifier.Crouch : PlayerModifier.Normal;
                }
            }
            
            if (InputManager.getInstance().GetKeyDown(PlayerActions.ToggleProne)){
                if (selectedAgentTransforms == null || selectedAgentTransforms.Count == 0) return;
                foreach (var controller in selectedAgentTransforms.Select(selectedAgentTransform => selectedAgentTransform.GetComponent<PlayerAnimationController>())){
                    controller.CurrentPlayerModifier = controller.CurrentPlayerModifier != PlayerModifier.Prone ? PlayerModifier.Prone : PlayerModifier.Normal;
                }
            }
            
            if (!InputManager.getInstance().GetKeyDown(PlayerActions.Inventory)) return;
            if (!inventoryIsOpen){
                if (selectedAgentTransforms.Count <= 2 && selectedAgentTransforms.Count > 0){
                    openInventories ??= new List<Inventory.scripts.Inventory>();
                    var inv1 = selectedAgentTransforms[0]
                        .GetComponent<Player>()
                        .inventory;
                    openInventories.Add(inv1);
                    inv1.showInventoryOnRight();

                    if (selectedAgentTransforms.Count == 2){
                        var inv2 = selectedAgentTransforms[1]
                            .GetComponent<Player>()
                            .inventory;
                        openInventories.Add(inv2);
                        inv2.showInventoryOnLeft();
                    }
                }
                inventoryIsOpen = true;
                GameManager.getInstance().MenuIsOpen();
                    
            } else {
                foreach (var openInventory in openInventories){
                    openInventory.closeInventory();
                }
                openInventories.Clear();
                inventoryIsOpen = false;
                GameManager.getInstance().MenuIsClosed();
            }
        }

        private void LateUpdate(){
            
            _ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(_ray, out _hit)) return;
            
            if (_hit.transform.CompareTag("Player")){
                _hit.transform.GetComponent<Player>().SetOnHover(true);
            }

            if (!Input.GetMouseButtonDown(0)) return;
            if (_hit.transform.CompareTag("Player")){
                setSelected(new List<Transform>{_hit.transform});
            } else if (_hit.transform.CompareTag("WalkableGround")){ 
                setDestination(_hit.point);
            } else if (_hit.transform.CompareTag("Enemy")){
                setEnemy(_hit.transform);
            }
        }

        private void setEnemy(Transform enemyTransform){
            foreach (var agentTransform in selectedAgentTransforms){
                agentTransform.GetComponent<Player>().AttackTarget(enemyTransform);
            }
        }

        private void setDestination(Vector3 targetDestination){
            if (selectedAgentTransforms == null) return;
            foreach (var agentTransform in selectedAgentTransforms.Where(t => t != null)){
                agentTransform.GetComponent<Player>().MoveToDestination(targetDestination);
            }
        }

        public void setSelected(List<Transform> newlySelectedAgents){
            if (inputManager.GetKey(PlayerActions.MultiSelect)){
                selectedAgentTransforms.AddRange(newlySelectedAgents);
                foreach (var selectedAgent in newlySelectedAgents
                    .Where(t => t != null)
                ){
                    selectedAgent.GetComponent<Player>().SetSelection(true);
                }
            }
            else{
                if (selectedAgentTransforms != null){
                    foreach (var p in selectedAgentTransforms
                        .Where(agentTransform => agentTransform != null)
                        .Select(agentTransform => agentTransform.GetComponent<Player>())
                    ){
                        p.SetSelection(false);
                        p.IsShownOnUi = false;
                    }
                }

                selectedAgentTransforms = newlySelectedAgents;
                foreach (var agentTransform in selectedAgentTransforms.Where(t => t != null)){
                    agentTransform.GetComponent<Player>().SetSelection(true);
                }

                if (selectedAgentTransforms.Count > 0){
                    selectedAgentTransforms[0].GetComponent<Player>().IsShownOnUi = true;
                }
            }
        }
    }
}
