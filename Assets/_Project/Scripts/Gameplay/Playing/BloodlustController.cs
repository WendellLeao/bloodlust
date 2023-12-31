using System.Collections;
using Bloodlust.Gameplay.Enemies;
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

        [Header("Drain Blood")] 
        [SerializeField]
        private int _drainBloodAmount;
        [SerializeField]
        private int _healMaxHealthAmount;
        [SerializeField] 
        private float _delayToDrainBlood = 1f;
        [SerializeField]
        private float _radius;

        private PlayerControls _playerControls;
        private PlayerControls.LandMapActions _landMap;
        private CharacterView _characterView;
        private IDamageable _damageable;
        private BloodChecker _bloodChecker;
        private CharacterMovement _movement;
        private IHasBlood _currentBloodTarget;
        private float _timer;
        private bool _isDrainingBlood;

        public bool IsDrainingBlood => _isDrainingBlood;
        
        public void Begin(PlayerControls playerControls, IDamageable damageable, CharacterView characterView, CharacterMovement movement)
        {
            _landMap = playerControls.LandMap;
            _damageable = damageable;
            _characterView = characterView;
            _movement = movement;
            
            _bloodChecker = new BloodChecker();
        }
        
        public void Stop()
        {}

        public void Tick(float deltaTime)
        {
            HandleGradualDamage(deltaTime);

            if (CanCheckForBloodToDrain())
            {
                CheckForBloodToDrain();
            }
        }

        public void DrainCurrentTargetBlood()
        {
            _isDrainingBlood = true;

            _damageable.SetIsInvulnerable(true);

            _characterView.TriggerDrainBlood();

            StartCoroutine(DrainBloodRoutine());

            IEnumerator DrainBloodRoutine()
            {
                yield return new WaitForSeconds(_delayToDrainBlood);

                _damageable.HealMaxHealth(_healMaxHealthAmount);
                _currentBloodTarget.DrainBlood(_drainBloodAmount, _damageable);

                _characterView.ResetOrderInLayer();
                
                _isDrainingBlood = false;
                _damageable.SetIsInvulnerable(false);
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
                    if (hitCollider.transform.TryGetComponent(out Enemy enemy))
                    {
                        enemy.IsBeingDrained = true;
                    }
                    
                    _currentBloodTarget = hasBlood;
                    
                    _damageable.SetIsInvulnerable(true);
                    
                    DashToTarget(hitCollider);
                }
            }
        }

        private void DashToTarget(Collider2D hitCollider)
        {
            _characterView.TriggerDash();

            Transform colliderTransform = hitCollider.transform;

            _movement.MoveTowardsPosition(colliderTransform);
        }
        
        private bool CanCheckForBloodToDrain()
        {
            if (_movement.HasTarget)
            {
                return false;
            }

            if (_isDrainingBlood)
            {
                return false;
            }
            
            if (!_landMap.Interact.triggered)
            {
                return false;
            }

            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}