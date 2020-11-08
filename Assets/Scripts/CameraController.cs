using UnityEngine;
using Cinemachine;

namespace AnimaRes
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualMainCam;
        [SerializeField] private CinemachineVirtualCamera virtualZoomCam;
        [SerializeField] private float zoomDistance;

        private void Start()
        {
            GameManager.Instance.OnSelected += SelectedHandler;
            GameManager.Instance.OnRestart += RestartHandler;
        }

        private void RestartHandler()
        {
            virtualZoomCam.Priority = virtualMainCam.Priority - 1;
        }

        private void SelectedHandler(Sphere sphere)
        {
            virtualZoomCam.transform.position = sphere.transform.position - (Vector3.forward * sphere.transform.localScale.z * zoomDistance);
            virtualZoomCam.Priority = virtualMainCam.Priority + 1;
        }

        private void OnDestroy()
        {
            virtualZoomCam.Priority = virtualMainCam.Priority;

            if (GameManager.InstanceExists)
            {
                GameManager.Instance.OnSelected -= SelectedHandler;
                GameManager.Instance.OnRestart -= RestartHandler;
            }
        }
    }
}