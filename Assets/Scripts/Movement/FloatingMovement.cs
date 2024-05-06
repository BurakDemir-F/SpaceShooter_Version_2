using UnityEngine;

namespace Movement
{
    public class FloatingMovement : MovementBehaviour
    {
        private bool _isMovingForward;
        public override void StartMovement()
        {
            _isMovingForward = true;
            MoveForward();
        }

        public override void EndMovement()
        {
            _transformUpdater.StopTransformChanges();
        }

        protected override void PositionReachedHandler()
        {
            if (_isMovingForward)
            {
                _isMovingForward = false;
                MoveUpAndDown();
            }
            else
            {
                _isMovingForward = true;
                MoveForward();
            }
        }

        private void MoveUpAndDown()
        {
            var midPoint = _borderProvider.GetMidPoint();
            var currentPosition = _transformUpdater.Transform.position;
            var currentPositionY = currentPosition.y;
            var verticalDistance = Vector3.Distance(_borderProvider.DownBorder, _borderProvider.TopBorder)/3f;
            var randomDistance = Random.Range(0f, verticalDistance);
            
            var nextY = midPoint.y > currentPosition.y
                ? currentPositionY + randomDistance
                : currentPositionY - randomDistance;

            var nextPos = new Vector3(currentPosition.x, nextY, currentPosition.z);
            var duration = randomDistance / _properties.MoveSpeed;
            _transformUpdater.MoveToPosition(nextPos,duration);
        }

        private void MoveForward()
        {
            var currentPosition = _transformUpdater.Transform.position;
            var horizontalLenght = Vector3.Distance(_borderProvider.LeftBorder, _borderProvider.RightBorder) / 3f;
            var randomDistance = Random.Range(0f, horizontalLenght);
            var nextPos = new Vector3(currentPosition.x - randomDistance, currentPosition.y, currentPosition.z);
            var duration = randomDistance / _properties.MoveSpeed;
            _transformUpdater.MoveToPosition(nextPos,duration);
        }
    }
}