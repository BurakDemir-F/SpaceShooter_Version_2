using UnityEngine;

namespace Movement
{
    public class PlayerTargetProvider : TargetProvider
    {
        private Transform _playerTransform;
        public override Vector3 GetTarget(Vector3 currentPos)
        {
            var playerPos = _playerTransform.position;
            var direction = Vector3.Normalize(playerPos - currentPos);
                
            return currentPos + direction * 100f;
        }

        public void SetPlayerTransform(Transform player)
        {
            _playerTransform = player;
        }
    }
}