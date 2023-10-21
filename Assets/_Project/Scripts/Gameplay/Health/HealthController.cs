using UnityEngine;

namespace Bloodlust.Gameplay.Health
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        public delegate void HealthDelegate(int currentHealth, int maxHealth);
        public event HealthDelegate OnHealthChanged;
        
        [SerializeField] 
        private int _maxHealth = 100;

        private int _currentHealth;
        
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;

        public void Begin()
        {
            _currentHealth = _maxHealth;
        }

        public void Stop()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int amount)
        {
            SetHealth(_currentHealth - amount);
        }

        public void Heal(int amount)
        {
            SetHealth(_currentHealth + amount);
        }

        private void SetHealth(int newHealth)
        {
            _currentHealth = Mathf.Clamp(newHealth, 0, _maxHealth);
            
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}
