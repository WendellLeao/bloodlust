using UnityEngine;

namespace Bloodlust.Gameplay.Health
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] 
        private float _maxHealth;

        private float _currentHealth;

        public void Begin()
        {
            _currentHealth = _maxHealth;
        }

        public void Stop()
        {
            _currentHealth = _maxHealth;
        }

        public void Tick(float deltaTime)
        { }

        public void TakeDamage(float amount)
        {
            SetHealth(_currentHealth - amount);
        }

        public void Heal(float amount)
        {
            SetHealth(_currentHealth + amount);
        }

        private void SetHealth(float newHealth)
        {
            Debug.Log($"<color=cyan>Old health: {_currentHealth} | New health: {newHealth}</color>");
            
            _currentHealth = newHealth;
            _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        }
    }
}
