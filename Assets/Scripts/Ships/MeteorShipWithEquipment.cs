using Border;
using Spawning;
using UnityEngine;

namespace Ships
{
    public class MeteorShipWithEquipment : MeteorShip
    {
        [SerializeField] private ShipWeaponEquipment _equipment;
        [SerializeField] private ShipHealthUI _equipmentHealthUI;

        public override void Construct(IBorderProvider borderProvider, ITargetProvider targetProvider, IPool pool)
        {
            base.Construct(borderProvider, targetProvider, pool);
            _equipmentHealthUI.Initialize(_equipment.ArmHealth);
        }

        public override void StartMoving()
        {
            base.StartMoving();
            _equipment.Construct();
            _equipment.StartFighting();
        }

        public override void Destruct()
        {
            base.Destruct();
            _equipment.Destruct();
            _equipment.StopFighting();
            _equipmentHealthUI.Destruct();
        }
    }
}