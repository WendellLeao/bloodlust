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
        [SerializeField] 
        private ScenesManager _scenesManager;

        public static GameplaySystem Instance;
        
        public CharacterManager CharacterManager => _characterManager;

        public ScenesManager ScenesManager => _scenesManager;

        private void Awake()
        {
            Instance = this;

            _characterManager.Initialize();
            _enemiesManager.Initialize();
            _scenesManager.Initialize();
        }

        private void OnDestroy()
        {
            _characterManager.Dispose();
            _enemiesManager.Dispose();
            _scenesManager.Dispose();
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
