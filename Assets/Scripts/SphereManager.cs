using System;
using UnityEngine;

namespace AnimaRes
{
    public class SphereManager : Singleton<SphereManager>
    {
        [SerializeField] private string selectionScene;

        public event Action<Sphere> OnSelected = delegate { };

        public Sphere SelectedSphere { get; private set; }

        private void OnEnable()
        {
            SceneLoader.OnSceneLoaded += SceneLoadedHandler;
        }

        public void SetSelectedSphere(Sphere sphere)
        {
            SelectedSphere = sphere;
            OnSelected?.Invoke(sphere);

            SceneLoader.Load(selectionScene);
            SceneLoader.MoveGameObjectToScene(sphere.gameObject, selectionScene);
        }

        private void SceneLoadedHandler(string sceneName)
        {
            if (sceneName == selectionScene)
            {
                foreach (var item in SceneLoader.AllAvailableGameObjectsByScene[sceneName])
                {
                    if (item != SelectedSphere.gameObject)
                    {
                        item.GetComponentInChildren<BaseTransition>().ExitTransition();
                    }
                }
            }
        }

        private void OnDisable()
        {
            SceneLoader.OnSceneLoaded -= SceneLoadedHandler;
        }
    }
}