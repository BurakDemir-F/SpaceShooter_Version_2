using Border;
using Spawning;
using UnityEngine;

namespace Ships
{
    public class MeteorShip : AutoFireShip
    {
        [SerializeField] private ShipHealthUI _shipHealthUI;
        [SerializeField] private TargetProvider _bulletTargetProvider;

        public override ITargetProvider BulletTargetProvider => _bulletTargetProvider;

        public override void Construct(IBorderProvider borderProvider, ITargetProvider targetProvider, IPool pool)
        {
            base.Construct(borderProvider, targetProvider, pool);
            _shipHealthUI.Initialize(_health);
        }

        public override void Destruct()
        {
            _shipHealthUI.Destruct();
            base.Destruct();
        }
    }
}