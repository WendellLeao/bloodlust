using Bloodlust.Gameplay.Health;
using Bloodlust.Inputs;
using UnityEngine;

namespace Bloodlust.Gameplay.Playing
{
    public class BloodlustController : MonoBehaviour
    {
        [Header("Gradual Damage")]
        [SerializeField] 
        private int _damageAmount;
        [SerializeField] 
        private float _damageRate;

        [Header("Check for Blood")]
        [SerializeField]
        private float _radius;

        private IDamageable _damageable;
        private PlayerControls _playerControls;
        private Collider2D[] _hitColliders = new Collider2D[5];
        private float _timer;
        private PlayerControls.LandMapActions _landMap;

        public void Begin(IDamageable damageable, PlayerControls playerControls)
        {
            _damageable = damageable;
            _landMap = playerControls.LandMap;
        }
        
        public void Stop()
        {}

        public void Tick(float deltaTime)
        {
            HandleGradualDamage(deltaTime);

            if (_landMap.Interact.triggered)
            {
                CheckForBlood();
            }
        }

        private void HandleGradualDamage(float deltaTime)
        {
            _timer += deltaTime;

            if (_timer >= _damageRate)
            {
                _damageable.TakeDamage(_damageAmount);
                _timer = 0f;
            }
        }
        
        private void CheckForBlood()
        {
            for (var i = 0; i < _hitColliders.Length; i++)
            {
                _hitColliders[i] = null;
            }
            
            int collidersCount = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _hitColliders);

            if (collidersCount <= 0)
            {
                return;
            }
            
            for (int i = 0; i < _hitColliders.Length; i++)
            {
                Collider2D hitCollider = _hitColliders[i];

                if (hitCollider == null)
                {
                    continue;
                }
                
                if (hitCollider.transform.TryGetComponent(out IHasBlood hasBlood))
                {
                    Transform colliderTransform = hitCollider.transform;

                    bool isFacingRight = colliderTransform.localScale.x > 0;
                    float offset = isFacingRight ? -0.5f : 0.5f;
                    
                    transform.position = colliderTransform.position + new Vector3(offset, 0.5f);

                    hasBlood.DrainBlood(amount: 9999, _damageable);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}