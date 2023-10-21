using Bloodlust.Gameplay.Health;
using UnityEngine;

namespace Bloodlust.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour, IHasBlood
    {
        [SerializeField]
        private HealthController _healthController;

        // TODO: remove hardcoded value
        public float DrainPower { get; } = 50;

        public void Begin()
        {
            _healthController.Begin();
        }

        public void Stop()
        {
            _healthController.Stop();
        }
        
        public void DrainBlood(int amount, IDamageable damageable)
        {
            _healthController.TakeDamage(amount);
            
            // TODO: remove hardcoded value
            damageable.Heal((int)DrainPower);
            
            if (_healthController.CurrentHealth <= 0)
            {
                Destroy(gameObject, 0.2f);
            }
            //
        }
    }
}