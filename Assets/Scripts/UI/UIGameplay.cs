using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AnimaRes
{
    public class UIGameplay : MonoBehaviour
    {
        [SerializeField] private GameObject titlePanel;
        [SerializeField] private Button restartButton;
        [SerializeField] private float titleDuration;

        public void InitiateTitle()
        {
            titlePanel.SetActive(true);
            DOTween.Sequence().SetDelay(titleDuration).AppendCallback(() => titlePanel.GetComponent<BaseTransition>().ExitTransition());
        }

        private void OnEnable()
        {
            SceneLoader.OnSceneLoaded += SceneLoadedHandler;
            SceneLoader.OnSceneUnloaded += SceneUnloadedHandler;
        }

        private void SceneUnloadedHandler(string sceneName)
        {
            if (sceneName == SceneLoader.THIRD_SCENE)
            {
                restartButton.gameObject.SetActive(false);
            }
            else if (sceneName == SceneLoader.SECOND_SCENE)
            {
                InitiateTitle();
            }
        }

        private void SceneLoadedHandler(string sceneName)
        {
            if (sceneName == SceneLoader.THIRD_SCENE)
            {
                restartButton.gameObject.SetActive(true);
            }
        }

        private void OnDisable()
        {
            SceneLoader.OnSceneLoaded -= SceneLoadedHandler;
            SceneLoader.OnSceneUnloaded -= SceneUnloadedHandler;
        }
    }
}