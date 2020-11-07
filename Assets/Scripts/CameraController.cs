using Cinemachine;
using UnityEngine;

namespace AnimaRes
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera virtualMainCam;
        [SerializeField] private CinemachineVirtualCamera virtualZoomCam;
        [SerializeField] private float distance;

        private void Start()
        {
            SphereManager.Instance.OnSelected += SelectedHandler;
        }

        private void SelectedHandler(Sphere sphere)
        {
            virtualZoomCam.transform.position = sphere.transform.position - (Vector3.forward * sphere.transform.localScale.z * distance);
            virtualZoomCam.Priority = virtualMainCam.Priority + 1;
        }

        private void OnDestroy()
        {
            virtualZoomCam.Priority = virtualMainCam.Priority;

            if (SphereManager.InstanceExists)
            {
                SphereManager.Instance.OnSelected -= SelectedHandler;
            }
        }
    }
}