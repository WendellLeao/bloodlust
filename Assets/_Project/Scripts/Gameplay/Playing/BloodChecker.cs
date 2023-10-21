using UnityEngine;

namespace Bloodlust.Gameplay.Playing
{
    public class BloodChecker
    {
        private Collider2D[] _hitColliders = new Collider2D[5];

        public Collider2D[] CheckForNearestBlood(Vector3 origin, float radius)
        {
            for (var i = 0; i < _hitColliders.Length; i++)
            {
                _hitColliders[i] = null;
            }
            
            Physics2D.OverlapCircleNonAlloc(origin, radius, _hitColliders);

            return _hitColliders;
        }
    }
}