using UnityEngine;

namespace AnimaRes
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float radius = 3;
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
                var targetXY = new Vector3(target.position.x, target.position.y, 0);

                var x = radius * Mathf.Cos(speed * Time.time);
                var y = radius * Mathf.Sin(speed * Time.time);
                var rotation = new Vector3(x, y, 0);

                _transform.position = targetXY + rotation;
            }
        }
    }
}