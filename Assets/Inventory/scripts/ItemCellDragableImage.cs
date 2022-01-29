using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Inventory.scripts{
    public class ItemCellDragableImage : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,
        IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler{

        internal InventoryCell inventoryCell;
        internal Image image;

        private Sprite tempImage;
        private bool imageTempActive;

        private bool ImageTempActive{ 
            get => imageTempActive;
            set{
                if (value){
                    image.sprite = tempImage;
                }else{
                    // if (!imageTempActive)
                        inventoryCell.ItemUpdated();
                }
                imageTempActive = value;
            }
        }

        private static bool isDragging;
        private static ItemCellDragableImage dragObject;
        
        private Vector2 startingAnchor;
        private Canvas canvas;
        private CanvasGroup canvasGroup;

        private RectTransform _transform;

        private bool started;
        public void Start(){
            if (!started){
                canvas = UIManager.getInstance().canvas;
                image = GetComponent<Image>();
                _transform = GetComponent<RectTransform>();
                canvasGroup = GetComponent<CanvasGroup>();
                started = true;
            }
        }
        
        public void OnPointerDown(PointerEventData eventData){
            //Debug.Log("OnPointerDown");
        }

        public void OnBeginDrag(PointerEventData eventData){
            if (inventoryCell.Slot.isFree) return;
            dragObject = this;
            isDragging = true;
            var anchoredPosition = _transform.anchoredPosition;
            startingAnchor = new Vector2(anchoredPosition.x, anchoredPosition.y);
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            //Debug.Log("OnBeginDrag");
        }

        public void OnEndDrag(PointerEventData eventData){
            showImage();
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            _transform.anchoredPosition = startingAnchor;
            //Debug.Log("OnEndDrag");
        }

        public void OnDrag(PointerEventData eventData){
            if (isDragging){
                _transform.anchoredPosition += eventData.delta / canvas.scaleFactor;
            }
        }

        public void OnDrop(PointerEventData eventData){
            if (!isDragging) return;
            isDragging = false;
            if (inventoryCell.Slot.isFree){
                if (eventData.pointerDrag != null){
                    var other = eventData.pointerDrag.GetComponent<ItemCellDragableImage>();
                    if (other != null && !other.inventoryCell.Slot.isFree){
                        inventoryCell.Slot.ItemRef = other.inventoryCell.Slot.ItemRef;
                        other.inventoryCell.Slot.ItemRef = null;
                    }
                }
            }else{
                Debug.Log("Slot is not free");
            }
        }

        public void OnPointerEnter(PointerEventData eventData){
            if (!isDragging || eventData.pointerDrag == null) return;
            var other = eventData
                .pointerDrag
                .GetComponent<ItemCellDragableImage>();
            if (other == null) return;
            var otherItem = other
                .inventoryCell
                .Slot
                .ItemRef;
            tempImage = otherItem != null ? otherItem.displayImage : InventoryCell.defaultSprite;

            ImageTempActive = true;
            dragObject.hideImage();
        }

        public void OnPointerExit(PointerEventData eventData){
            ImageTempActive = false;
            if (isDragging)
                dragObject.showImage();
        }

        private void hideImage(){
            image.enabled = false;
        }

        private void showImage(){
            image.enabled = true;
        }
        
    }
}
