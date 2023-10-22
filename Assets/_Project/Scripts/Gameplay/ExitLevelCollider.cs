using UnityEngine;
using UnityEngine.SceneManagement;
using Character = Bloodlust.Gameplay.Playing.Character;

namespace Bloodlust.Gameplay
{
    public class ExitLevelCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform.TryGetComponent(out Character character))
            {
                LoadNextScene();
            }
        }

        private void LoadNextScene()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(nextSceneIndex + 1);
        }
    }
}