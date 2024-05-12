using Movement;
using Ships.Weapons;
using UnityEngine;

namespace Ships
{
    public class ShipWeaponEquipment : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        [SerializeField] private ShipHealth _health;
        [SerializeField] private ShipTrigger _shipTrigger;
        [SerializeField] private TransformUpdater _armTransformUpdater;
        [SerializeField] private float _speedInAngle;

        public ShipHealth ArmHealth => _health;
        public void Construct()
        {
            _shipTrigger.OnHit += OnHitHandler;
            _health.OnExplode += OnExplodeHandler;
            Activate();
        }

        public void Destruct()
        {
            _shipTrigger.OnHit -= OnHitHandler;
            _health.OnExplode -= OnExplodeHandler;
        }

        public void Activate()
        {
            _armTransformUpdater.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _armTransformUpdater.gameObject.SetActive(false);
        }
        
        public void StartFighting()
        {
            _armTransformUpdater.RotateContinuously(_speedInAngle);
        }

        public void StopFighting()
        {
            _armTransformUpdater.StopTransformChanges();
        }

        private void OnHitHandler(Weapon weapon)
        {
            _health.Damage(weapon.Damage);
        }

        private void OnExplodeHandler()
        {
            Deactivate();
        }
    }
}