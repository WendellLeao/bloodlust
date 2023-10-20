using UnityEngine;

namespace Bloodlust.UI
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField]
        private UIGameplayView _uiGameplayView;
        
        public static UISystem Instance;
        
        private void Awake()
        {
            Instance = this;

            _uiGameplayView.Initialize();
        }
        
        private void OnDestroy()
        {
            _uiGameplayView.Dispose();
        }
    }
}
