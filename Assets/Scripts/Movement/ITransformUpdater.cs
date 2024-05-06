using System;
using UnityEngine;

namespace Movement
{
    public interface ITransformUpdater
    {
        event Action OnPositionReached;
        event Action OnRotationReached;
        Transform Transform { get; }
        void SetPosition(Vector3 position);
        void Translate(Vector3 translation);
        void MoveToPosition(Vector3 position, float duration);
        void Rotate(float angle);
        void RotateContinuously(float speedInAngle, float duration = -1f);
        void StopTransformChanges();
    }
}