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
        private bool _isFacingRight = true;

        public void Begin(PlayerControls playerControls)
        {
            _playerControls = playerControls;
        }
        
        public void Stop()
        {}

        public void Tick(float deltaTime)
        {
            _move = GetNormalizedMove();

            HandleFlip();
        }

        public void FixedTick(float fixedDeltaTime)
        {
            _rigidbody.velocity = _move * (fixedDeltaTime * _moveSpeed);
        }

        private void HandleFlip()
        {
            if (_isFacingRight && _move.x <= -1f || !_isFacingRight && _move.x >= 1f)
            {
                Transform myTransform = transform;
                Vector3 localScale = myTransform.localScale;

                float localScaleX = localScale.x;
                localScaleX *= -1f;
                myTransform.localScale = new Vector3(localScaleX, localScale.y, localScale.z);

                _isFacingRight = !_isFacingRight;
            }
        }

        private Vector2 GetNormalizedMove()
        {
            PlayerControls.LandMapActions landMap = _playerControls.LandMap;

            Vector2 move = landMap.Move.ReadValue<Vector2>();
            
            return move.normalized;
        }
    }
}