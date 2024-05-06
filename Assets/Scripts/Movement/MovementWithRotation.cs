using UnityEngine;

namespace Movement
{
    public class MovementWithRotation : MovementBehaviour
    {
        public override void StartMovement()
        {
            var targetPos = GetTargetPosition();
            
            var duration = Vector3.Distance(targetPos, transform.position) / _properties.MoveSpeed;
            _transformUpdater.MoveToPosition(targetPos, duration);
            _transformUpdater.RotateContinuously(_properties.RotateSpeedInAngle);
        }

        public override void EndMovement()
        {
            _transformUpdater.StopTransformChanges();
        }
    }
}