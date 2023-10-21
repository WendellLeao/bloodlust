using Bloodlust.Gameplay.Health;
using UnityEngine;

namespace Bloodlust.Gameplay.Playing
{
    public class BloodlustController : MonoBehaviour
    {
        [SerializeField] 
        private int _damageAmount;
        [SerializeField] 
        private float _damageRate;

        private IDamageable _damageable;
        private float _timer;

        public void Begin(IDamageable damageable)
        {
            _damageable = damageable;
        }
        
        public void Stop()
        {}

        public void Tick(float deltaTime)
        {
            _timer += deltaTime;

            if (_timer >= _damageRate)
            {
                _damageable.TakeDamage(_damageAmount);
                _timer = 0f;
            }
        }
    }
}