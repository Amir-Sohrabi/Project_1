using UnityEngine;

namespace _Scripts.Core.Camera
{
    /// <summary>
    /// Smoothly follows a target using damping.
    /// Designed for third-person and top-down cameras.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class SmoothFollowCamera : MonoBehaviour
    {
        [Header("Target")]
        [SerializeField] private Transform target;

        [Header("Position")]
        [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -10f);
        [SerializeField] private float smoothTime = 0.2f;

        [Header("Rotation")]
        [SerializeField] private bool lookAtTarget = true;
        [SerializeField] private float rotationSmoothTime = 0.1f;

        private Vector3 _positionVelocity;
        private Vector3 _rotationVelocity;

        private void LateUpdate()
        {
            if (target is null) return;

            SmoothPosition();
            if (lookAtTarget)
                SmoothRotation();
        }

        private void SmoothPosition()
        {
            var desiredPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(
                transform.position,
                desiredPosition,
                ref _positionVelocity,
                smoothTime
            );
        }

        private void SmoothRotation()
        {
            var direction = target.position - transform.position;
            if (direction.sqrMagnitude < 0.001f) return;

            var targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime / rotationSmoothTime
            );
        }
    }
}