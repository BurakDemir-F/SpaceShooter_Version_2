using System;
using Ships.Weapons;
using UnityEngine;

namespace Ships
{
    public class PlayerShip : BulletShip
    {
        [SerializeField] private KeyCode _fireKeyKode = KeyCode.Space;
        private bool _isBulletFired;
        private float _bulletTimeCounter;
        public event Action OnEnemyHit;
        
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

        protected override void BulletExplodeHandler(SpaceShip bullet, WeaponType weaponType)
        {
            if(weaponType == WeaponType.Ship)
            {
                OnEnemyHit?.Invoke();
                Debug.Log("on enemy hit.");
            }

            base.BulletExplodeHandler(bullet,weaponType);
        }
    }
}