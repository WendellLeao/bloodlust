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

        private bool IsAlive => _healthController.CurrentHealth > 0f;

        public void Begin(PlayerControls playerControls)
        {
            _playerControls = playerControls;
            _playerControls.Enable();
            
            _characterView.Begin();
            
            _movement.Begin(_playerControls, _characterView);
            _bloodlustController.Begin(_playerControls, _healthController, _characterView, _movement);
            _healthController.Begin();

            _healthController.OnDeath += HandleDeath;
            _movement.OnReachTarget += HandleReachTarget;
        }

        public void Stop()
        {
            _playerControls.Disable();
            _movement.Stop();
            _healthController.Stop();
            _bloodlustController.Stop();
            _characterView.Stop();
            
            _healthController.OnDeath -= HandleDeath;
            _movement.OnReachTarget -= HandleReachTarget;
        }

        public void Tick(float deltaTime)
        {
            if (CanMove())
            {
                _movement.Tick(deltaTime);
            }

            _bloodlustController.Tick(deltaTime);
        }

        public void FixedTick(float fixedDeltaTime)
        {
            _movement.FixedTick(fixedDeltaTime);
        }

        private void HandleDeath()
        {
            GameplaySystem gameplaySystem = GameplaySystem.Instance;
            
            gameplaySystem.ScenesManager.ReloadActiveScene();
        }

        private void HandleReachTarget()
        {
            _bloodlustController.DrainCurrentTargetBlood();
        }
        
        private bool CanMove()
        {
            if (!IsAlive)
            {
                return false;
            }
            
            if(_bloodlustController.IsDrainingBlood)
            {
                return false;
            }

            return true;
        }
    }
}
