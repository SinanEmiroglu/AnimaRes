using UnityEngine;

namespace AnimaRes
{
    public class ExtraGameObject : MonoBehaviour
    {
        [SerializeField] private BaseTransition transition;

        private void OnEnable()
        {
            GameManager.Instance.OnRestart += RestartHandler;
            transition.OnFinalEnd.AddListener(HandleFinalTransition);
        }

        private void HandleFinalTransition()
        {
            Destroy(gameObject);
        }

        private void RestartHandler()
        {
            transition.ExitTransition();
        }

        private void OnDisable()
        {
            transition.OnFinalEnd.RemoveAllListeners();
            GameManager.Instance.OnRestart -= RestartHandler;
        }
    }
}