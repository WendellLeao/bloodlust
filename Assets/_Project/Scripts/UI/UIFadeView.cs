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

        private bool _hasFade;

        public void Begin()
        {
            _hasFade = false;
        }
        
        public void Stop()
        { }
        
        public void FadeIn()
        {
            if (_hasFade)
            {
                return;
            }
            
            _fadeInImage.gameObject.SetActive(true);
            _fadeOutImage.gameObject.SetActive(false);

            _fadeInImage.fillAmount = 0f;
            _fadeInImage.DOFillAmount(1f, _fadeDuration);
            
            _hasFade = true;
        }
        
        public void FadeOut()
        {
            if (_hasFade)
            {
                return;
            }
        
            _fadeOutImage.gameObject.SetActive(true);
            _fadeInImage.gameObject.SetActive(false);

            _fadeOutImage.fillAmount = 1f;
            _fadeOutImage.DOFillAmount(0f, _fadeDuration);

            _hasFade = true;
        }
    }
}