using System;

namespace AnimaRes
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action OnRestart = delegate { };
        public event Action<Sphere> OnSelected = delegate { };

        public Sphere SelectedSphere { get; private set; }

        public void SetSelectedSphere(Sphere sphere)
        {
            SelectedSphere = sphere;
            OnSelected?.Invoke(sphere);

            SceneLoader.Load(SceneLoader.THIRD_SCENE);
            SceneLoader.MoveGameObjectToScene(sphere.transform.root.gameObject, SceneLoader.THIRD_SCENE);
        }

        public void Restart()
        {
            OnRestart?.Invoke();
        }
    }
}