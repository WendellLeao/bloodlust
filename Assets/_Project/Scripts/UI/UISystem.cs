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

#if !UNITY_EDITOR
            _uiFadeView.FadeOut();
#endif
        }
        
        private void OnDestroy()
        {
            _uiGameplayView.Dispose();
        }
    }
}
