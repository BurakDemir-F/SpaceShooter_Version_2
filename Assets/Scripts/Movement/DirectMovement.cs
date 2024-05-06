using UnityEngine;

namespace Movement
{
    public class DirectMovement : MovementBehaviour
    {
        public override void StartMovement()
        {
            var target = GetTargetPosition();
            var distance = Vector3.Distance(target, _transformUpdater.Transform.position);
            var duration = distance / _properties.MoveSpeed;
            _transformUpdater.MoveToPosition(target, duration);
        }

        public override void EndMovement()
        {
            _transformUpdater.StopTransformChanges();
        }
    }
}