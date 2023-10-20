using UnityEngine;

namespace Bloodlust.UI
{
    public class UIGameplayView : MonoBehaviour
    {
        [SerializeField] 
        private UIHealthBar _uiHealthBar;

        public void Initialize()
        {
            _uiHealthBar.Begin();
        }

        public void Dispose()
        {
            _uiHealthBar.Stop();
        }
    }
}