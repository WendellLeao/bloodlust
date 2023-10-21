using System;
using Bloodlust.Inputs;
using UnityEngine;

namespace Bloodlust.Gameplay.Playing
{
    public class CharacterMovement : MonoBehaviour
    {
        public event Action OnReachTarget;
        
        [SerializeField] 
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private float _moveTowardsSpeed;
        
        private PlayerControls _playerControls;
        private CharacterView _characterView;
        private Vector2 _move;
        private Transform _targetPosition;

        public void Begin(PlayerControls playerControls, CharacterView characterView)
        {
            _playerControls = playerControls;
            _characterView = characterView;
        }
        
        public void Stop()
        {}

        public void Tick(float deltaTime)
        {
            if (_targetPosition != null)
            {
                _move = Vector3.zero;

                HandleMoveTorwardsTarget(deltaTime);
                
                return;
            }
            
            _move = GetNormalizedMove();

            _characterView.HandleFlip(_move);
            _characterView.HandleRunningAnimation(_move);
        }

        public void FixedTick(float fixedDeltaTime)
        {
            if (_targetPosition != null)
            {
                _rigidbody.velocity = Vector3.zero;
                
                return;
            }

            _rigidbody.velocity = _move * (fixedDeltaTime * _moveSpeed);
        }

        public void MoveTowardsPosition(Transform target)
        {
            _targetPosition = target;
        }
        
        private void HandleMoveTorwardsTarget(float deltaTime)
        {
            float step = _moveTowardsSpeed * deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, _targetPosition.position, step);
            
            if (Vector3.Distance(transform.position, _targetPosition.position) <= 0.1f)
            {
                OnReachTarget?.Invoke();
                _targetPosition = null;
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