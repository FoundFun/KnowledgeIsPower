using UnityEngine;

namespace CodeBase.Enemy
{
    public class RotateToHero : Follow
    {
        public float Speed;

        private Transform _heroTransform;
        private Vector3 _positionToLook;

        private void Update()
        {
            if (Initialized())
                RotateTowardsHero();
        }

        public void Construct(Transform heroTransform) =>
            _heroTransform = heroTransform;

        private void RotateTowardsHero()
        {
            UpdatePositionToLookAt();

            transform.rotation = SmoothedRotation(transform.rotation, _positionToLook);
        }

        private void UpdatePositionToLookAt()
        {
            Vector3 positionDiff = _heroTransform.position - transform.position;
            _positionToLook = new Vector3(positionDiff.x, _positionToLook.y, positionDiff.z);
        }

        private Quaternion SmoothedRotation(Quaternion transformRotation, Vector3 positionToLook) =>
            Quaternion.Lerp(transformRotation, TargetRotation(positionToLook), SpeedFactor());

        private Quaternion TargetRotation(Vector3 positionToLook) =>
            Quaternion.LookRotation(positionToLook);

        private float SpeedFactor() =>
            Speed * Time.deltaTime;

        private bool Initialized() =>
            _heroTransform != null;
    }
}