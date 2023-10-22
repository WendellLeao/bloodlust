using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bloodlust.Gameplay.Enemies
{
    public class EnemyPatrol : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _startWaitTime;
        [SerializeField] private Transform[] _moveSpots;
        
        [SerializeField] private EnemyAnimator _myAnimator;
        [SerializeField] private bool _isFacingRight = true;

        private float _waitTime;
        private int _nextSpot = 0;

        private bool _moveDirectionTowards = true;

        private void Awake()
        {
            _waitTime = _startWaitTime;
        }

        void FixedUpdate()
        {
            Move();
        }

        private void SetNewSpot()
        {
            _nextSpot += _moveDirectionTowards ? 1 : -1;

            if (_nextSpot == _moveSpots.Length - 1 || _nextSpot == 0)
            {
                _moveDirectionTowards = !_moveDirectionTowards;
            }
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, _moveSpots[_nextSpot].position,
                _movementSpeed * Time.deltaTime);
            
            ResetWaitingTime();
        }

        private void ResetWaitingTime()
        {
            if (Vector2.Distance(transform.position, _moveSpots[_nextSpot].position) < 0.2f)
            {
                if (_waitTime <= 0)
                {
                    SetNewSpot();
                    Vector3 targetPosition = _moveSpots[_nextSpot].position;
                    StartCoroutine(Flip(targetPosition, 0f));
                    _waitTime = _startWaitTime;
                    _myAnimator.HandleRunningAnimation(true);
                }
                else
                {
                    _myAnimator.HandleRunningAnimation(false);

                    _waitTime -= Time.deltaTime;
                }
            }
        }

        private IEnumerator Flip(Vector3 targetPosition, float delay)
        {
            yield return new WaitForSeconds(delay);

            Vector3 myPosition = transform.position;

            if (targetPosition.x < myPosition.x)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _isFacingRight = false;
            }
            else if (targetPosition.x > transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _isFacingRight = true;
            }
        }
    }
}
