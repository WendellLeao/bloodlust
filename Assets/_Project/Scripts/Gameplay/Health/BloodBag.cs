using UnityEngine;

namespace Bloodlust.Gameplay.Health
{
    public class BloodBag : MonoBehaviour
    {
        [SerializeField] 
        private int _healAmount;
        [SerializeField] 
        private int _healMaxHealthAmount;

        [Header("DEBUG")] 
        [SerializeField] private bool _destroyOnCollide = true;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.HealMaxHealth(_healMaxHealthAmount);
                damageable.Heal(_healAmount);

                if (_destroyOnCollide)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}