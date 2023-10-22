using UnityEngine;

namespace Bloodlust.UI
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField]
        private UIGameplayView _uiGameplayView;
        [SerializeField] 
        private UIFadeView _uiFadeView;
        
        public static UISystem Instance;

        public UIFadeView UIFadeView => _uiFadeView;

        private void Awake()
        {
            Instance = this;

            _uiGameplayView.Initialize();

            _uiFadeView.FadeOut();
        }
        
        private void OnDestroy()
        {
            _uiGameplayView.Dispose();
        }
    }
}
