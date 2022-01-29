using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI{
    public class EnemyHealthBarController : MonoBehaviour, IBarController{
        [SerializeField] private Image fillImage;
        [SerializeField] private float updateSpeed;
        public void SetValue(float current, float max){
            StartCoroutine(HealthChanged(current / max));
        }

        private IEnumerator HealthChanged(float value){
            var perChange = fillImage.fillAmount;
            var elapsed = 0f;

            while (elapsed < updateSpeed){
                elapsed += Time.deltaTime;
                fillImage.fillAmount = Mathf.Lerp(perChange, value, elapsed / updateSpeed);
                yield return null;
            }

            fillImage.fillAmount = value;
        }
    }
}
