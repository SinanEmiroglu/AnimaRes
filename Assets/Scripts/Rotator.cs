using UnityEngine;

namespace AnimaRes
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float speed = 100f;
        [SerializeField] private Transform target;

        public bool IsRotating { get; set; } = true;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (IsRotating)
            {
                _transform.RotateAround(target.position, Vector3.forward, speed * Time.deltaTime);
            }
        }
    }
}