using UnityEngine;

namespace AnimaRes
{
    public class Sphere : MonoBehaviour
    {
        [SerializeField] private Rotator rotator;
        [SerializeField] private GameObject extraGameObject;

        private void OnEnable()
        {
            SceneLoader.OnSceneLoaded += SceneLoadedHandler;
        }

        private void SceneLoadedHandler(string sceneName)
        {
            if (sceneName == "Scene_3")
            {
                rotator.IsRotating = false;
            }
        }

        private void OnMouseUp()
        {
            if (rotator != null)
            {
                rotator.IsRotating = false;
            }

            SphereManager.Instance.SetSelectedSphere(this);
        }

        private void OnDisable()
        {
            SceneLoader.OnSceneLoaded -= SceneLoadedHandler;
        }
    }
}