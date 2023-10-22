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

        public void Tick(float deltaTime)
        {
            foreach (Enemy enemy in _enemies)
            {
                if (enemy == null)
                {
                    continue;
                }
                
                enemy.Tick(deltaTime);
            }
        }
    }
}
