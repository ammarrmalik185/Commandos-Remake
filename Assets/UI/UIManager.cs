using UnityEngine;
using UnityEngine.UI;

namespace UI{
    public class UIManager : MonoBehaviour{
        [SerializeField] private GameObject healthBarGameObject;
        public IBarController playerHealthBar;
        public Image characterIcon;
        
        [HideInInspector] public GameObject inventoryGrid;
        public GameObject rightInventoryGrid;
        public GameObject leftInventoryGrid;

        public Canvas canvas;
        private static UIManager _instance;

        private void Awake(){
            _instance = this;
            playerHealthBar = healthBarGameObject.GetComponent<IBarController>();
            canvas = GetComponentInChildren<Canvas>();
        }

        public static UIManager getInstance(){
            return _instance;
        }
    }
}
