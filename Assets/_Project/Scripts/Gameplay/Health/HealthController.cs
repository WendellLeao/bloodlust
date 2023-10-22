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
        private bool _healthWillDeplete;
        
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public bool HealthWillDeplete => _healthWillDeplete;

        public void Begin()
        {
            _currentHealth = _maxHealth;
            _healthWillDeplete = false;
        }

        public void Stop()
        { }

        public void TakeDamage(int amount)
        {
            int newHealth = _currentHealth - amount;
            
            _healthWillDeplete = newHealth <= 0f;
            
            SetHealth(newHealth);
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
