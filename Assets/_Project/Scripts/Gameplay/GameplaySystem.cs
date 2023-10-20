using Bloodlust.Gameplay.Playing;
using UnityEngine;

namespace Bloodlust.Gameplay
{
    public class GameplaySystem : MonoBehaviour
    {
        [SerializeField] 
        private CharacterManager _characterManager;

        public CharacterManager CharacterManager => _characterManager;

        public static GameplaySystem Instance;
        
        private void Awake()
        {
            Instance = this;

            _characterManager.Initialize();
        }

        private void OnDestroy()
        {
            _characterManager.Dispose();
        }

        private void Update()
        {
            _characterManager.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _characterManager.FixedTick(Time.fixedDeltaTime);
        }
    }
}
