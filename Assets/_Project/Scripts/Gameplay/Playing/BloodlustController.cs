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
        private PlayerControls.LandMapActions _landMap;
        private BloodChecker _bloodChecker;
        private float _timer;

        public void Begin(IDamageable damageable, PlayerControls playerControls)
        {
            _damageable = damageable;
            _landMap = playerControls.LandMap;

            _bloodChecker = new BloodChecker();
        }
        
        public void Stop()
        {}

        public void Tick(float deltaTime)
        {
            HandleGradualDamage(deltaTime);

            if (_landMap.Interact.triggered)
            {
                CheckForBloodToDrain();
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
        
        private void CheckForBloodToDrain()
        {
            Collider2D[] hitColliders = _bloodChecker.CheckForNearestBlood(transform.position, _radius);
            
            for (int i = 0; i < hitColliders.Length; i++)
            {
                Collider2D hitCollider = hitColliders[i];

                if (hitCollider == null)
                {
                    continue;
                }
                
                if (hitCollider.transform.TryGetComponent(out IHasBlood hasBlood))
                {
                    DrainColliderBlood(hitCollider, hasBlood);
                }
            }
        }

        private void DrainColliderBlood(Collider2D hitCollider, IHasBlood hasBlood)
        {
            Transform colliderTransform = hitCollider.transform;

            bool isFacingRight = colliderTransform.localScale.x > 0;
            float offset = isFacingRight ? -0.2f : 0.2f;

            transform.position = colliderTransform.position + new Vector3(offset, 0f);

            hasBlood.DrainBlood(amount: 9999, _damageable);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}