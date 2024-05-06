using UnityEngine;

namespace Movement
{
    public class InputBasedMovement : MovementBehaviour
    {
        private bool _isAllowedToMove;
        
        public override void StartMovement()
        {
            _isAllowedToMove = true;
        }

        public override void EndMovement()
        {
            _isAllowedToMove = false;
        }

        private void Update()
        {
            if (!_isAllowedToMove)
                return;

            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var currentPos = _transformUpdater.Transform.position;
            var xPos = horizontal * _properties.MoveSpeed * Time.deltaTime;
            var yPos = vertical * _properties.MoveSpeed * Time.deltaTime;
            _transformUpdater.Translate(new Vector3(xPos,yPos,0f));

            var newPos = _transformUpdater.Transform.position;
            var size = _sizeProvider.Size;
            if (!_borderProvider.IsInBorder(newPos,size))
            {
                _transformUpdater.SetPosition(_borderProvider.GetNearestPositionInBorder(newPos,size));
            }
        }
    }
}