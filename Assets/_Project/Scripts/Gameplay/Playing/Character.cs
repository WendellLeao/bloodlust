using Bloodlust.Gameplay.Health;
using Bloodlust.Inputs;
using UnityEngine;

namespace Bloodlust.Gameplay.Playing
{
    public class Character : MonoBehaviour
    {
        [SerializeField] 
        private CharacterMovement _movement;
        [SerializeField]
        private HealthController _healthController;

        [SerializeField] 
        private BloodlustController _bloodlustController;

        private PlayerControls _playerControls;

        public HealthController HealthController => _healthController;

        public void Begin(PlayerControls playerControls)
        {
            _playerControls = playerControls;
            _playerControls.Enable();
            
            _movement.Begin(_playerControls);
            _healthController.Begin();
            _bloodlustController.Begin(_healthController, _playerControls);
        }

        public void Stop()
        {
            _playerControls.Disable();
            _movement.Stop();
            _healthController.Stop();
            _bloodlustController.Stop();
        }

        public void Tick(float deltaTime)
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.K))
            {
                _healthController.TakeDamage(10);
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                _healthController.Heal(40);
            }
#endif
            
            _movement.Tick(deltaTime);
            _bloodlustController.Tick(deltaTime);
        }
        
        public void FixedTick(float fixedDeltaTime)
        {
            _movement.FixedTick(fixedDeltaTime);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
