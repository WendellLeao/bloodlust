using UnityEngine;

namespace Bloodlust.Gameplay.Enemies
{
    public class EnemiesManager : MonoBehaviour
    {
        [SerializeField]
        private Enemy[] _enemies;

        public void Initialize()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.Begin();
            }
        }

        public void Dispose()
        {
            foreach (Enemy enemy in _enemies)
            {
                enemy.Stop();
            }
        }
    }
}
