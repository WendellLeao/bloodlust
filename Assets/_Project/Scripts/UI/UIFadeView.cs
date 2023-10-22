using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Bloodlust.UI
{
    public class UIFadeView : MonoBehaviour
    {
        [SerializeField] 
        private Image _fadeInImage;
        [SerializeField] 
        private Image _fadeOutImage;
        [SerializeField] 
        private float _fadeDuration;

        public void FadeIn()
        {
            _fadeInImage.gameObject.SetActive(true);
            _fadeOutImage.gameObject.SetActive(false);

            _fadeInImage.fillAmount = 0f;
            _fadeInImage.DOFillAmount(1f, _fadeDuration);
        }
        
        public void FadeOut()
        {
            _fadeOutImage.gameObject.SetActive(true);
            _fadeInImage.gameObject.SetActive(false);

            _fadeOutImage.fillAmount = 1f;
            _fadeOutImage.DOFillAmount(0f, _fadeDuration);
        }
    }
}