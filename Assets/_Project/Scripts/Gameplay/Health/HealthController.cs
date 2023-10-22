using System;
using UnityEngine;

namespace Bloodlust.Gameplay.Health
{
    public class HealthController : MonoBehaviour, IDamageable
    {
        public delegate void HealthDelegate(int currentHealth, int maxHealth);
        public event HealthDelegate OnHealthChanged;

        public event Action OnDeath;
        
        [SerializeField] 
        private int _maxHealth = 100;

        private int _originalMaxHealth;
        private int _currentHealth;
        private bool _healthWillDeplete;
        
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _maxHealth;
        public int OriginalMaxHealth => _originalMaxHealth;
        public bool HealthWillDeplete => _healthWillDeplete;

        public void Begin()
        {
            _originalMaxHealth = _maxHealth;
            _currentHealth = _originalMaxHealth;
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
        
        public void HealMaxHealth(int amount)
        {
            SetMaxHealth(_maxHealth + amount);
        }

        public void ReduceMaxHealth(int amount)
        {
            SetMaxHealth(_maxHealth - amount);
        }
        
        private void SetHealth(int newHealth)
        {
            _currentHealth = Mathf.Clamp(newHealth, 0, _maxHealth);
            
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
        
        private void SetMaxHealth(int newMaxHealth)
        {
            _maxHealth = Mathf.Clamp(newMaxHealth, 0, _originalMaxHealth);

            if (_currentHealth <= 0f)
            {
                OnDeath?.Invoke();
            }
            
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }
}
