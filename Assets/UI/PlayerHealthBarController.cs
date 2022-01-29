using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI{
    public class PlayerHealthBarController : MonoBehaviour, IBarController{
        private Image fillImage;
        [SerializeField] private float updateSpeed;
        
        void Start(){
            fillImage = GetComponent<Image>();
        }

        public void SetValue(float current, float max){
            
            StartCoroutine(HealthChanged(current / max));
        }

        private IEnumerator HealthChanged(float value){
            var perChange = fillImage.fillAmount;
            var elapsed = 0f;

            while (elapsed < updateSpeed){
                elapsed += Time.deltaTime;
                var fillAmount = Mathf.Lerp(perChange, value, elapsed / updateSpeed);
                fillImage.fillAmount = Mathf.Lerp(perChange, value, elapsed / updateSpeed);
                fillImage.color = Color.HSVToRGB(fillAmount * (120f / 360), 1, 1);
                yield return null;
            }

            fillImage.fillAmount = value;
            fillImage.color = Color.HSVToRGB(value * (120f / 360), 1, 1);
        }
    }
}
