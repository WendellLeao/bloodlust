using System.Collections;
using Bloodlust.Gameplay;
using Bloodlust.Gameplay.Health;
using Bloodlust.Gameplay.Playing;
using UnityEngine;
using UnityEngine.UI;

namespace Bloodlust.UI
{
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] 
        private Image _healthBarImage;
        [SerializeField] 
        private Image _maxHealthBarImage;

        private HealthController _characterHealthController;

        public void Begin()
        {
            StartCoroutine(WaitForGameplayInstanceRoutine());

            IEnumerator WaitForGameplayInstanceRoutine()
            {
                while (GameplaySystem.Instance == null)
                {
                    yield return null;
                }

                GameplaySystem gameplaySystem = GameplaySystem.Instance;
                
                Character character = gameplaySystem.CharacterManager.Character;
                
                _characterHealthController = character.HealthController;
                _characterHealthController.OnHealthChanged += HandleCharacterHealthChanged;
            }
        }

        public void Stop()
        {
            _characterHealthController.OnHealthChanged -= HandleCharacterHealthChanged;
        }
        
        private void HandleCharacterHealthChanged(int currentHealth, int maxHealth)
        {
            _healthBarImage.fillAmount = (float)currentHealth / _characterHealthController.OriginalMaxHealth;
            _maxHealthBarImage.fillAmount = (float)maxHealth / _characterHealthController.OriginalMaxHealth;
        }
    }
}