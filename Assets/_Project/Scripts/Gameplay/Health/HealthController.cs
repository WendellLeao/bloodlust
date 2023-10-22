using System;
using System.Collections;
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
        [SerializeField] 
        private float _invulnerabilityTime;

        private Coroutine _invulnerabilityRoutine;
        private int _originalMaxHealth;
        private int _currentHealth;
        private bool _healthWillDeplete;
        private bool _isInvulnerable;

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
            if (_isInvulnerable)
            {
                return;
            }

            int newHealth = _currentHealth - amount;
            
            _healthWillDeplete = newHealth <= 0f;
            
            SetHealth(newHealth);
        }

        public void ReduceMaxHealth(int amount)
        {
            if (_isInvulnerable)
            {
                return;
            }
            
            SetMaxHealth(_maxHealth - amount);
        }

        public void Heal(int amount)
        {
            SetHealth(_currentHealth + amount);

            if (_invulnerabilityRoutine != null)
            {
                StopCoroutine(_invulnerabilityRoutine);
            }
            
            _invulnerabilityRoutine = StartCoroutine(TriggerInvulnerabilityOnHealRoutine());
        }
        
        public void HealMaxHealth(int amount)
        {
            SetMaxHealth(_maxHealth + amount);
        }

        public void SetIsInvulnerable(bool isInvulnerable)
        {
            if (_invulnerabilityRoutine != null)
            {
                StopCoroutine(_invulnerabilityRoutine);
            }

            _isInvulnerable = isInvulnerable;
        }

        private void SetHealth(int newHealth)
        {
            _currentHealth = Mathf.Clamp(newHealth, 0, _maxHealth);
            
            if (_currentHealth <= 0f)
            {
                OnDeath?.Invoke();
            }
            
            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
        
        private void SetMaxHealth(int newMaxHealth)
        {
            _maxHealth = Mathf.Clamp(newMaxHealth, 0, _originalMaxHealth);

            OnHealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
        
        private IEnumerator TriggerInvulnerabilityOnHealRoutine()
        {
            _isInvulnerable = true;

            yield return new WaitForSeconds(_invulnerabilityTime);

            _isInvulnerable = false;
        }
    }
}
