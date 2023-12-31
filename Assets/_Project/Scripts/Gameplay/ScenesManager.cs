using System.Collections;
using Bloodlust.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bloodlust.Gameplay
{
    public class ScenesManager : MonoBehaviour
    {
        [SerializeField] 
        private float _loadNextSceneDelay;

        private bool _isLoadingScene;

        public bool IsLoadingScene => _isLoadingScene;

        public void Initialize()
        {
            _isLoadingScene = false;
        }
        
        public void Dispose()
        {}

        public void ReloadActiveScene()
        {
            int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;

            StartCoroutine(LoadSceneRoutine(activeSceneIndex, _loadNextSceneDelay));
        }
        
        public void LoadNextScene()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (SceneManager.GetActiveScene().name.Contains("Lvl_015"))
            {
                Application.Quit();
            }
            
            StartCoroutine(LoadSceneRoutine(nextSceneIndex, _loadNextSceneDelay));
        }

        private IEnumerator LoadSceneRoutine(int sceneIndex, float delay)
        {
            _isLoadingScene = true;
            
            UISystem uiSystem = UISystem.Instance;
            uiSystem.UIFadeView.FadeIn();

            yield return new WaitForSeconds(delay);

            SceneManager.LoadScene(sceneIndex);
        }
    }
}