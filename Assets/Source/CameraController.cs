using UnityEngine;

namespace Assets.Source
{
    public class CameraController : MonoBehaviour
    {
        public float MoveSpeedMinZoom, MoveSpeedMaxZoom;
        public float RotationSpeed;
        public float StickMinZoom, StickMaxZoom;
        public float SwivelMinZoom, SwivelMaxZoom;
        //public HexGrid Grid;

        private float _zoom = 1f;
        private float _rotationAngle;
        private Transform _swivel, _stick;

        void Awake()
        {
            _swivel = transform.GetChild(0);
            _stick = _swivel.GetChild(0);
        }

        void Update()
        {
            var zoomDelta = Input.GetAxis("Mouse ScrollWheel");
            if (zoomDelta != 0f) AdjustZoom(zoomDelta);

            var rotationDelta = Input.GetAxis("Rotation");
            if (rotationDelta != 0f) AdjustRotation(rotationDelta);

            var xDelta = Input.GetAxis("Horizontal");
            var zDelta = Input.GetAxis("Vertical");
            if (xDelta != 0f || zDelta != 0f) AdjustPosition(xDelta, zDelta);
        }

        private void AdjustPosition(float xDelta, float zDelta)
        {
            var direction = transform.localRotation * new Vector3(xDelta, 0f, zDelta).normalized;
            var damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(zDelta));
            var moveSpeed = Mathf.Lerp(MoveSpeedMinZoom, MoveSpeedMaxZoom, _zoom);
            var distance = moveSpeed * damping * Time.deltaTime;

            var position = transform.localPosition;
            position += direction * distance;
            transform.localPosition = ClampPosition(position);
        }

        private Vector3 ClampPosition(Vector3 position)
        {
            //var cellCountX = Grid.ChunkCountX * HexMetrics.ChunkSizeX;
            //var xMax = (cellCountX - 0.5f) * (2f * HexMetrics.InnerRadius);
            //position.x = Mathf.Clamp(position.x, 0f, xMax);

            //var cellCountZ = Grid.ChunkCountZ * HexMetrics.ChunkSizeZ;
            //var zMax = (cellCountZ - 1f) * (2f * HexMetrics.OuterRadius);
            //position.z = Mathf.Clamp(position.z, 0f, zMax);

            return position;
        }

        private void AdjustRotation(float delta)
        {
            _rotationAngle += delta * RotationSpeed * Time.deltaTime;
            _rotationAngle = (_rotationAngle + 360f) % 360f;
            transform.localRotation = Quaternion.Euler(0f, _rotationAngle, 0f);
        }

        private void AdjustZoom(float delta)
        {
            _zoom = Mathf.Clamp01(_zoom + delta);

            var distance = Mathf.Lerp(StickMinZoom, StickMaxZoom, _zoom);
            _stick.localPosition = new Vector3(0f, 0f, distance);

            var angle = Mathf.Lerp(SwivelMinZoom, SwivelMaxZoom, _zoom);
            _swivel.localRotation = Quaternion.Euler(angle, 0f, 0f);
        }
    }
}