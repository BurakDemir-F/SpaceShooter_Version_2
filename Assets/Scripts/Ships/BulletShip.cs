using Ships.Weapons;
using UnityEngine;

namespace Ships
{
    public class BulletShip : SpaceShip
    {
        [SerializeField] protected string _bulletPoolKey;
        [SerializeField] protected float _bulletSpawnInterval;
        
        protected void SpawnBullet()
        {
            var bullet = _pool.Get<Bullet>(_bulletPoolKey);
            bullet.transform.position = _weapon.FireTransform.position;
            bullet.Construct(_borderProvider,_targetProvider,null);
            bullet.OnExplode += BulletExplodeHandler;
            bullet.StartMoving();
        }

        protected virtual void BulletExplodeHandler(SpaceShip bullet, WeaponType hitBy)
        {
            bullet.OnExplode -= BulletExplodeHandler;
            bullet.Destruct();
            bullet.GameObject.SetActive(false);
            _pool.Return(bullet);
        }
    }
}