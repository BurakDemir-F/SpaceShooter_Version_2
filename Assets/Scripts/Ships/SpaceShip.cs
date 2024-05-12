using System;
using Border;
using Movement;
using Ships.Weapons;
using Spawning;
using UnityEngine;

namespace Ships
{
    public abstract class SpaceShip : MonoBehaviour, IPoolObject
    {
        [field:SerializeField] public ShipType ShipType { get; private set; } 
        protected MovementBehaviour _movementBehaviour;
        protected ShipTrigger _shipTrigger;
        protected IBorderProvider _borderProvider;
        protected ITargetProvider _targetProvider;
        protected IPool _pool;
        protected ShipHealth _health;
        protected Weapon _weapon;
        public string Key { get; set; }
        public IPool Pool { get; set; }
        public GameObject GameObject { get; set; }
        public ShipHealth ShipHealth => _health;
        public event Action<SpaceShip,WeaponType> OnExplode;

        protected bool IsMoving;
        protected WeaponType _weaponHitBy;
        public void Construct(IBorderProvider borderProvider,ITargetProvider targetProvider,IPool pool)
        {
            _movementBehaviour = GetComponent<MovementBehaviour>();
            _health = GetComponent<ShipHealth>();
            _shipTrigger = GetComponentInChildren<ShipTrigger>();
            _weapon = GetComponentInChildren<Weapon>();
            _borderProvider = borderProvider;
            _targetProvider = targetProvider;
            _pool = pool;
            
            _movementBehaviour.Construct(borderProvider,targetProvider);
            _health.RefreshHealth();
            _health.OnExplode += ExplodeHandler;
            _shipTrigger.OnHit += HitHandler;
            _health.OnHealthChanged += HealthChangeHandler;
        }

        public virtual void Destruct()
        {
            _movementBehaviour.Destruct();
            _health.OnExplode -= ExplodeHandler;
            _shipTrigger.OnHit -= HitHandler;
            _health.OnHealthChanged -= HealthChangeHandler;
            IsMoving = false;
        }

        public virtual void StartMoving()
        {
            _movementBehaviour.StartMovement();
            IsMoving = true;
        }

        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
        }

        public void OnReturnToPool()
        {
            _weaponHitBy = WeaponType.None;
        }

        private void ExplodeHandler()
        {
            OnExplode?.Invoke(this, _weaponHitBy);
        }

        private void HitHandler(Weapon weapon)
        {
            _weaponHitBy = weapon.WeaponType;
            _health.Damage(weapon.Damage);
        }

        private void HealthChangeHandler(int currentHealth)
        {
            
        }
    }
}