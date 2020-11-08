using UnityEngine;

namespace AnimaRes
{
    public class Sphere : MonoBehaviour
    {
        [SerializeField] private Rotator rotator;
        [SerializeField] private BaseTransition transition;
        [SerializeField] private ExtraGameObject extraGameObjectPrefab;

        private Sphere _selected;

        private void OnEnable()
        {
            GameManager.Instance.OnSelected += SelectedHandler;
            GameManager.Instance.OnRestart += RestartHandler;

            transition.OnFinalEnd.AddListener(HandleFinalEndTransition);
        }

        private void RestartHandler()
        {
            transition.ExitTransition();
        }

        private void HandleFinalEndTransition()
        {
            if (_selected == this)
            {
                SceneLoader.MoveGameObjectToScene(transform.root.gameObject, SceneLoader.SECOND_SCENE);
                SceneLoader.Unload(SceneLoader.THIRD_SCENE);
                SceneLoader.Unload(SceneLoader.SECOND_SCENE);
            }
        }

        private void SelectedHandler(Sphere selection)
        {
            _selected = selection;

            if (rotator != null)
            {
                rotator.IsRotating = false;
            }

            if (selection != this && transition != null)
            {
                transition.ExitTransition();
            }

            if (selection == this && extraGameObjectPrefab != null)
            {
                var spawned = Instantiate(extraGameObjectPrefab, transform);
                spawned.transform.position = transform.position + Vector3.one;
                spawned.transform.localScale = transform.localScale * 0.5f;
            }
        }

        private void OnMouseUp()
        {
            GameManager.Instance.SetSelectedSphere(this);
        }

        private void OnDisable()
        {
            transition.OnFinalEnd.RemoveAllListeners();

            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnSelected -= SelectedHandler;
                GameManager.Instance.OnRestart -= RestartHandler;
            }
        }
    }
}