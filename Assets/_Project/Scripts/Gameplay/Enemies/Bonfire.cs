using UnityEngine;

namespace Bloodlust.Gameplay.Enemies
{
    public class Bonfire : MonoBehaviour
    {
        [SerializeField] 
        private EnemyLamp _enemyLamp;

        private void Update()
        {
            _enemyLamp.Tick(Time.deltaTime);
        }
    }
}