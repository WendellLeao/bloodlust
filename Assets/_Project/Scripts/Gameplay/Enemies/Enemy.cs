using Bloodlust.Gameplay.Health;
using UnityEngine;

namespace Bloodlust.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour, IHasBlood
    {
        [SerializeField] 
        private EnemyLamp _enemyLamp;
        [SerializeField]
        private HealthController _healthController;
        
        public float DrainPower { get; } = 50;

        public void Begin()
        {
            _healthController.Begin();
            _enemyLamp.Begin();
        }

        public void Stop()
        {
            _healthController.Stop();
            _enemyLamp.Stop();
        }

        public void Tick(float deltaTime)
        {
            _enemyLamp.Tick(deltaTime);
        }
        
        public void DrainBlood(int amount, IDamageable damageable)
        {
            _healthController.TakeDamage(amount);
            
            damageable.Heal((int)DrainPower);
            
            if (_healthController.CurrentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        public IDamageable GetDamageable()
        {
            return _healthController;
        }
    }
}