using System.Collections;
using UnityEngine;

namespace Ships
{
    public class AutoFireShip : BulletShip
    {
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
    }
}