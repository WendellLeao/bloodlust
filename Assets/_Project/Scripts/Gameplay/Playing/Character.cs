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

        private PlayerControls _playerControls;

        public void Begin(PlayerControls playerControls)
        {
            _playerControls = playerControls;
            _playerControls.Enable();
            
            _movement.Begin(_playerControls);
            _healthController.Begin();
        }

        public void Stop()
        {
            _playerControls.Disable();
            _movement.Stop();
            _healthController.Stop();
        }

        public void Tick(float deltaTime)
        {
            _movement.Tick(deltaTime);
            _healthController.Tick(deltaTime);
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
