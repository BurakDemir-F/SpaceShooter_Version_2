using System;
using Movement;
using UnityEngine;

namespace Background
{
    public class BackgroundImage : MonoBehaviour
    {
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;
        [SerializeField] private Transform _center;
        private ITransformUpdater _transformUpdater;
        
        public Vector3 StartPosition => _start.position;
        public Vector3 EndPosition => _end.position;
        public Vector3 CenterPosition => _center.position;
        public float Length => Vector3.Magnitude(_end.position - _start.position);

        public event Action<BackgroundImage> OnPositionReached;

        public void Construct()
        {
            _transformUpdater = GetComponent<ITransformUpdater>();
            _transformUpdater.OnPositionReached += PositionReachedHandler;
        }

        public void Destruct()
        {
            _transformUpdater.OnPositionReached -= PositionReachedHandler;
        }

        public void SetPosition(Vector3 position)
        {
            _transformUpdater.SetPosition(position);
        }
        
        public void MoveToPosition(Vector3 targetPos, float speed)
        {
            var duration = Vector3.Magnitude(targetPos - _transformUpdater.Transform.position) / speed;
            _transformUpdater.MoveToPosition(targetPos,duration);
        }

        public void StopMovement()
        {
            _transformUpdater.StopTransformChanges();
        }

        private void PositionReachedHandler()
        {
            OnPositionReached?.Invoke(this);
        }
        
    }
}