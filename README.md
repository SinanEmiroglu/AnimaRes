# AnimaRes

## 1. Answer
For a modular and generic transition, we must first examine what a transition is and how it works. There are two main stages in a basic transition. These are the entrance to the transition and the exit from the transition, respectively. In this task, I created a generic base class that can be implemented by different subclasses for different types of transitions. It doesn't have to be fade animation but it can also be scale or rotate right now. An event is called for each transition state and it is very easy to subscribe to these events from both the editor and the script. In this way, one transition can be easily connected to another to implement chain seamless behaviors. Moreover, a transition script works independently and modularly on UI or mesh objects added to it. This means that there is no need to cache objects in the transition logic that we want to apply.

## 2. Answer
The main challenge I had during development was to handle dependencies in-between additive scenes. The approach I tried to implement was a little bit amatuer. A scene manager-script controls scene loading and unloading as well as objects' movement among scenes. Nevertheless, I definitely aware a robust solution can be designed for additive scene dependency management. In addition, orbiting spheres was a bit harder than I expected. Unity has a built-in method -Transform.RotateAround()- to orbit; however, this is not properly working for objects orbiting around a moving object. Therefore, I implemented an orbiting logic by utilizing trigonometry which took some time to figure out.

## 3. Answer
```
public class SceneLoader : MonoBehaviour
{
        public static int GetActiveSceneCount { get; }
        public static event Action<string> OnSceneLoaded;
        public static event Action<string> OnSceneUnloaded;
        public static Dictionary<string, GameObject[]> AllAvailableGameObjectsByScene;

        public static void MoveGameObjectToScene(GameObject gameObject, string sceneName)
        public static void Load(string sceneName)
        public static void Unload(string sceneName)
}

public class BaseTransition : MonoBehaviour
{
        public UnityEvent OnInitialBegin;
        public UnityEvent OnInitialEnd;
        public UnityEvent OnFinalBegin;
        public UnityEvent OnFinalEnd;

        protected Tween beginTween;
        protected Tween endTween;

        public void EnterTransition()
        public void ExitTransition()

        private IEnumerator BeginCoroutine()
        private IEnumerator EndCoroutine()
}

public class Fade : BaseTransition
{
        [SerializeField] private new Renderer renderer;
        [SerializeField] private float fadeInDuration;
        [SerializeField] private float fadeOutDuration;

        private void OnEnable() //Caching Tweens
}

public class UIFade : BaseTransition
{
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float fadeInDuration;
        [SerializeField] private float fadeOutDuration;

        private void OnEnable() //Caching Tweens
}

public class CameraController : MonoBehaviour
{
        [SerializeField] private CinemachineVirtualCamera virtualMainCam;
        [SerializeField] private CinemachineVirtualCamera virtualZoomCam;
        [SerializeField] private float zoomDistance;
 
        private void RestartHandler()
        private void SelectedHandler(Sphere sphere)
}

public class Sphere : MonoBehaviour
{
        [SerializeField] private Rotator rotator;
        [SerializeField] private BaseTransition transition;
        [SerializeField] private ExtraGameObject extraGameObjectPrefab;

        private void RestartHandler()
        private void HandleFinalEndTransition()
        private void SelectedHandler(Sphere selection)
        private void OnMouseUp()
}
```
## 4. Answer
According to the definition, each clean coded method should do only one thing. These are the two example methods written in the task project.
```
//Under BaseTransition script
private IEnumerator BeginCoroutine()
{
  OnInitialBegin?.Invoke();
  yield return beginTween.Play().WaitForCompletion();
  OnInitialEnd?.Invoke();
}

//Under UITimeline script
private void UpdateTimeLine()
{
  timelineSlider.value = SceneLoader.GetActiveSceneCount;
  sceneIndexText.text = timelineSlider.value.ToString();
}
```
