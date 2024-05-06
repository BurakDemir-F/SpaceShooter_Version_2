using System.Collections;
using Ships.Weapons;
using UnityEngine;

namespace Ships
{
    public class GunShip : SpaceShip
    {
        [SerializeField] private string _bulletPoolKey;
        [SerializeField] private float _bulletSpawnInterval;
        [SerializeField] private float _startWait = 3f;
        
        public override void StartMoving()
        {
            base.StartMoving();
            StartCoroutine(SpawnBulletCor());
        }

        public override void Destruct()
        {
            base.Destruct();
            StopAllCoroutines();
        }

        private IEnumerator SpawnBulletCor()
        {
            var wait = new WaitForSeconds(_bulletSpawnInterval);
            yield return new WaitForSeconds(_startWait);
            while (IsMoving)
            {
                SpawnBullet();
                yield return wait;
            }
        }

        private void SpawnBullet()
        {
            var bullet = _pool.Get<Bullet>(_bulletPoolKey);
            bullet.transform.position = _weapon.FireTransform.position;
            bullet.Construct(_borderProvider,_targetProvider,null);
            bullet.OnExplode += BulletExplodeHandler;
            bullet.StartMoving();
        }

        private void BulletExplodeHandler(SpaceShip bullet)
        {
            bullet.OnExplode -= BulletExplodeHandler;
            bullet.Destruct();
            bullet.GameObject.SetActive(false);
            _pool.Return(bullet);
        }
    }
}