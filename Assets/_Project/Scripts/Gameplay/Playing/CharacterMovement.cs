using Bloodlust.Inputs;
using UnityEngine;

namespace Bloodlust.Gameplay.Playing
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] 
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _moveSpeed;
        
        private PlayerControls _playerControls;
        private Vector2 _move;

        public void Begin(PlayerControls playerControls)
        {
            _playerControls = playerControls;
        }
        
        public void Stop()
        {}

        public void Tick(float deltaTime)
        {
            _move = GetNormalizedMove();
        }
        
        public void FixedTick(float fixedDeltaTime)
        {
            _rigidbody.velocity = _move * (fixedDeltaTime * _moveSpeed);
        }

        private Vector2 GetNormalizedMove()
        {
            PlayerControls.LandMapActions landMap = _playerControls.LandMap;

            Vector2 move = landMap.Move.ReadValue<Vector2>();
            
            return move.normalized;
        }
    }
}