using Bloodlust.Gameplay.Health;
using UnityEngine;

namespace Bloodlust.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private HealthController _healthController;

        public void Begin()
        {
            _healthController.Begin();
        }

        public void Stop()
        {
            _healthController.Stop();
        }
    }
}