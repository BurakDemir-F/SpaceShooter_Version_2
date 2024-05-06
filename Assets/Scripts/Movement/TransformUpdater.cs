using System;
using System.Collections;
using UnityEngine;

namespace Movement
{
    public class TransformUpdater : MonoBehaviour, ITransformUpdater
    {
        public event Action OnPositionReached;
        public event Action OnRotationReached;
        public Transform Transform => transform;
        private Coroutine _movementCor;
        private Coroutine _rotationCor;

        private bool _isMoving;
        private bool _isRotating;
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void Translate(Vector3 position)
        {
            transform.Translate(position);
        }

        public void MoveToPosition(Vector3 position, float duration)
        {
            if(_isMoving)
                return;
            
            _isMoving = true;
            _movementCor = StartCoroutine(MovementCor(transform.position, position, duration));
        }

        public void Rotate(float angle)
        {
            transform.Rotate(Vector3.forward,angle);
        }

        public void StopTransformChanges()
        {
            StopAllCoroutines();
            _isMoving = false;
            _isRotating = false;
        }

        public void RotateContinuously(float speedInAnglePerSecond, float duration = -1f)
        {
            if(_isRotating)
                return;

            _isRotating = true;
            _rotationCor =
                StartCoroutine(RotationCor(transform.rotation.eulerAngles.z, speedInAnglePerSecond, duration));
        }

        private IEnumerator MovementCor(Vector3 currentPos, Vector3 targetPos , float duration)
        {
            var elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                transform.position = Vector3.Lerp(currentPos, targetPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _isMoving = false;
            OnPositionReached?.Invoke();
        }

        private IEnumerator RotationCor(float currentRotation, float speedInAnglePerSecond, float duration = -1f)
        {
            var rotateForever = duration == -1;
            if (rotateForever)
            {
                while (_isRotating)
                {
                    Rotate(speedInAnglePerSecond * Time.deltaTime);
                    yield return null;
                }
                
                OnRotationReached?.Invoke();
                _isRotating = false;
                yield break;
            }
            
            var elapsedTime = 0f;
            var rotatedAngle = 0f;
            while (elapsedTime < duration)
            {
                var rotation = speedInAnglePerSecond * Time.deltaTime; 
                Rotate(rotation);
                elapsedTime += Time.deltaTime;
                rotatedAngle += rotation;
                Debug.Log($"rotation: {rotatedAngle}");
                yield return null;
            }
            
            OnRotationReached?.Invoke();
            _isRotating = false;
        }
    }
}