using Bloodlust.Inputs;
using UnityEngine;

namespace Bloodlust.Gameplay.Playing
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] 
        private Character _character;
        [SerializeField] 
        private Transform _spawnPoint;

        private PlayerControls _playerControls;

        public Character Character => _character;
        
        public void Initialize()
        {
            _playerControls = new PlayerControls();
            
            _character.SetPosition(_spawnPoint.position);
            _character.Begin(_playerControls);
        }

        public void Dispose()
        {
            _character.Stop();
        }

        public void Tick(float deltaTime)
        {
            _character.Tick(deltaTime);
        }

        public void FixedTick(float fixedDeltaTime)
        {
            _character.FixedTick(fixedDeltaTime);
        }
    }
}