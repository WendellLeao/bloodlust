using Bloodlust.Gameplay.Enemies;
using Bloodlust.Gameplay.Playing;
using UnityEngine;

namespace Bloodlust.Gameplay
{
    public class GameplaySystem : MonoBehaviour
    {
        [SerializeField] 
        private CharacterManager _characterManager;
        [SerializeField] 
        private EnemiesManager _enemiesManager;

        public CharacterManager CharacterManager => _characterManager;

        public static GameplaySystem Instance;
        
        private void Awake()
        {
            Instance = this;

            _characterManager.Initialize();
            _enemiesManager.Initialize();
        }

        private void OnDestroy()
        {
            _characterManager.Dispose();
            _enemiesManager.Dispose();
        }

        private void Update()
        {
            _characterManager.Tick(Time.deltaTime);
            _enemiesManager.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _characterManager.FixedTick(Time.fixedDeltaTime);
        }
    }
}
