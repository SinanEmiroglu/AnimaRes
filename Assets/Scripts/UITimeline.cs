using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace AnimaRes
{
    public class UITimeline : MonoBehaviour
    {
        [SerializeField] private Slider timelineSlider;
        [SerializeField] private TextMeshProUGUI sceneIndexText;

        private void OnEnable()
        {
            SceneLoader.OnSceneLoaded += SceneLoadedHandler;
            SceneLoader.OnSceneUnloaded += SceneUnloadedHandler;
        }

        private void SceneUnloadedHandler(string sceneName)
        {
            timelineSlider.value = SceneLoader.GetActiveSceneCount;
            sceneIndexText.text = timelineSlider.value.ToString();
        }

        private void SceneLoadedHandler(string sceneName)
        {
            timelineSlider.value = SceneLoader.GetActiveSceneCount;
            sceneIndexText.text = timelineSlider.value.ToString();
        }

        private void OnDisable()
        {
            SceneLoader.OnSceneLoaded -= SceneLoadedHandler;
        }
    }
}