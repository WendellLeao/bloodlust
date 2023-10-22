using UnityEngine;

namespace Bloodlust.Gameplay.Health
{
    public class BloodBag : MonoBehaviour
    {
        [SerializeField] 
        private int _healAmount;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out IDamageable damageable))
            {
                damageable.Heal(_healAmount);

                Destroy(gameObject);
            }
        }
    }
}