using UnityEngine;
using Character = Bloodlust.Gameplay.Playing.Character;

namespace Bloodlust.Gameplay
{
    public class ExitLevelCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out Character _))
            {
                GameplaySystem gameplaySystem = GameplaySystem.Instance;
            
                gameplaySystem.ScenesManager.LoadNextScene();
            }
        }
    }
}