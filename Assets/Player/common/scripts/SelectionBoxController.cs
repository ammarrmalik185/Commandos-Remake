using System;
using System.Collections.Generic;
using Managers.GameManager;
using Player.common.scripts;
using UnityEngine;

namespace Player{
    public class SelectionBoxController : MonoBehaviour{
        private PlayerManager manager;

        public RectTransform selectionBox;

        private Vector2 startPosition;
        private Camera mainCamera;
        private common.scripts.Player[] players;
        // Start is called before the first frame update
        void Start(){
            mainCamera = Camera.main;
            manager = GetComponent<PlayerManager>();
            players = GetComponentsInChildren<common.scripts.Player>();
        }

        // Update is called once per frame
        void Update(){

            if (GameManager.getInstance().canSelectionBoxWork){
                if (Input.GetMouseButtonDown(0)){
                    startPosition = Input.mousePosition;
                }

                if (Input.GetMouseButton(0)){
                    UpdateSelectionBox(Input.mousePosition);
                }

                if (Input.GetMouseButtonUp(0)){
                    ReleaseSelectionBox();
                }
            }else{
                selectionBox.gameObject.SetActive(false);
            }
        }

        private void ReleaseSelectionBox(){
            selectionBox.gameObject.SetActive(false);
            var min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
            var max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);

            var selectionPlayers = new List<Transform>();
            foreach (var player in players){
                if (player.isDead) return;
                var screenPos = mainCamera.WorldToScreenPoint(player.gameObject.transform.position);
                if (screenPos.x > min.x &&
                    screenPos.y > min.y &&
                    screenPos.x < max.x &&
                    screenPos.y < max.y){
                    selectionPlayers.Add(player.gameObject.transform);
                }
            }
            if (selectionPlayers.Count > 0)
                manager.setSelected(selectionPlayers);
        }

        void UpdateSelectionBox(Vector2 curMousePosition){
            if (!selectionBox.gameObject.activeInHierarchy)
                selectionBox.gameObject.SetActive(true);
            var width = curMousePosition.x - startPosition.x;
            var height = curMousePosition.y - startPosition.y;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
            selectionBox.anchoredPosition = startPosition + new Vector2(width / 2, height / 2);
        }
    }
}
