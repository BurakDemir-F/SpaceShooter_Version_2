using System;
using UnityEngine;

namespace Ships
{
    public class PlayerShip : BulletShip
    {
        [SerializeField] private KeyCode _fireKeyKode = KeyCode.Space;
        private bool _isBulletFired;
        private float _bulletTimeCounter;
        private void Update()
        {
            if(!IsMoving)
                return;

            if (_isBulletFired)
            {
                _bulletTimeCounter += Time.deltaTime;
            }

            var fireKeyPressed = Input.GetKeyDown(_fireKeyKode);
            if(!fireKeyPressed)
                return;

            if (_isBulletFired && _bulletTimeCounter < _bulletSpawnInterval)
                return;

            if (_isBulletFired && _bulletTimeCounter >= _bulletSpawnInterval)
            {
                _bulletTimeCounter = 0f;
            }
            
            SpawnBullet();
            _isBulletFired = true;
        }
    }
}