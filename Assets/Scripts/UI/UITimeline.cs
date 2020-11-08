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
            UpdateTimeLine();
        }

        private void SceneLoadedHandler(string sceneName)
        {
            UpdateTimeLine();
        }

        private void UpdateTimeLine()
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