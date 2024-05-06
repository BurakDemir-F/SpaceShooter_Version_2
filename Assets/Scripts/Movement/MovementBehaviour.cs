using System;
using Border;
using UnityEngine;

namespace Movement
{
    public abstract class MovementBehaviour : MonoBehaviour
    {
        [SerializeField] protected MovementProperties _properties;
        protected ITransformUpdater _transformUpdater;
        protected IBorderProvider _borderProvider;
        protected ITargetProvider _targetProvider;
        protected ISizeProvider _sizeProvider;
        protected bool IsEnteredBorders { get; set; }
        public abstract void StartMovement();
        public abstract void EndMovement();
        public event Action<Vector2> OnMovementDirectionChanged; 

        public void Construct(IBorderProvider borderProvider, ITargetProvider targetProvider)
        {
            _borderProvider = borderProvider;
            _targetProvider = targetProvider;
            _sizeProvider = GetComponent<ISizeProvider>();
            _transformUpdater = GetComponent<ITransformUpdater>();
            IsEnteredBorders = false;
            _transformUpdater.OnPositionReached += PositionReachedHandler;
            _transformUpdater.OnRotationReached += RotationReachedHandler;
        }

        public void Destruct()
        {
            _transformUpdater.OnPositionReached -= PositionReachedHandler;
            _transformUpdater.OnRotationReached -= RotationReachedHandler;
            _transformUpdater.StopTransformChanges();
        }

        protected Vector3 GetTargetPosition()
        {
            return _targetProvider.GetTarget(_transformUpdater.Transform.position);
        }

        protected virtual void PositionReachedHandler()
        {
            EndMovement();
        }

        protected virtual void RotationReachedHandler()
        {
            
        }
    }
}