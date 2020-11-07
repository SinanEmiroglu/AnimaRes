using UnityEngine;
using UnityEngine.SceneManagement;

namespace AnimaRes
{
    public class SceneLoader : MonoBehaviour
    {
        public void Load(int sceneIndex)
        {
            SceneManager.sceneLoaded += SceneLoadedHandler;
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }

        private void SceneLoadedHandler(Scene scene, LoadSceneMode mode)
        {
            MoveAfterLoad();
            SceneManager.sceneLoaded -= SceneLoadedHandler;
        }

        private void MoveAfterLoad()
        {
        }
    }
}