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
        private BloodlustController _bloodlustController;
        [SerializeField]
        private HealthController _healthController;
        [SerializeField]
        private CharacterView _characterView;

        private PlayerControls _playerControls;

        public HealthController HealthController => _healthController;

        public void Begin(PlayerControls playerControls)
        {
            _playerControls = playerControls;
            _playerControls.Enable();
            
            _characterView.Begin();
            
            _movement.Begin(_playerControls, _characterView);
            _bloodlustController.Begin(_playerControls, _healthController, _characterView, _movement);
            _healthController.Begin();

            _movement.OnReachTarget += HandleReachTarget;
        }

        public void Stop()
        {
            _playerControls.Disable();
            _movement.Stop();
            _healthController.Stop();
            _bloodlustController.Stop();
            _characterView.Stop();
            
            _movement.OnReachTarget -= HandleReachTarget;
        }

        public void Tick(float deltaTime)
        {
            if (!_bloodlustController.IsDrainingBlood)
            {
                _movement.Tick(deltaTime);
            }

            _bloodlustController.Tick(deltaTime);
        }
        
        public void FixedTick(float fixedDeltaTime)
        {
            _movement.FixedTick(fixedDeltaTime);
        }

        private void HandleReachTarget()
        {
            _bloodlustController.DrainCurrentTargetBlood();
        }
    }
}
