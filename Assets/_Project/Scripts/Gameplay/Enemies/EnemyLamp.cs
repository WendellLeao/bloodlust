using System;
using System.Collections;
using Bloodlust.Gameplay.Health;
using Bloodlust.Gameplay.Playing;
using UnityEngine;

namespace Bloodlust.Gameplay.Enemies
{
    public class EnemyLamp : MonoBehaviour
    {
        [SerializeField] 
        private int _damageAmount;
        [SerializeField] 
        private int _maxHealthDamageAmount;
        [SerializeField] 
        private float _delayToDealDamage;
        [SerializeField]
        private float _damageRate;

        private Character _character;
        private Coroutine _delayRoutine;
        private float _timer;

        public void Begin()
        { }

        public void Stop()
        { }
        
        public void Tick(float deltaTime)
        {
            if (_character != null)
            {
                _timer += deltaTime;

                if (_timer >= _damageRate)
                {
                    HealthController characterHealth = _character.HealthController;
                    
                    characterHealth.ReduceMaxHealth(_maxHealthDamageAmount);
                    characterHealth.TakeDamage(_damageAmount);
                    
                    _timer = 0f;
                }
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out Character character))
            {
                if (_delayRoutine != null)
                {
                    StopCoroutine(_delayRoutine);
                }
                
                _delayRoutine = StartCoroutine(DelayToDealDamage(character));
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out Character _))
            {
                if (_delayRoutine != null)
                {
                    StopCoroutine(_delayRoutine);
                }

                _character = null;
            }
        }

        private IEnumerator DelayToDealDamage(Character character)
        {
            yield return new WaitForSeconds(_delayToDealDamage);

            _character = character;
        }
    }
}