using System;
using Ships.Weapons;
using UnityEngine;

namespace Ships
{
    public class ShipTrigger : MonoBehaviour
    {
        [SerializeField] private Collider2D _thisCollider;
        public event Action<Weapon> OnHit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_thisCollider == other)
                return;
            //CheckObject(other);
            
            if(!other.TryGetComponent<WeaponProvider>(out var weaponProvider))
                return;
            
            OnHit?.Invoke(weaponProvider.Get());
        }

        private void CheckObject(Collider2D other)
        {
            var layer = _thisCollider.gameObject.layer;
            if (layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log($"player hit with collider{LayerMask.LayerToName(other.gameObject.layer)}",_thisCollider.gameObject);
            }
            else if(layer == LayerMask.NameToLayer("Ship"))
            {
                Debug.Log($"ship hit with collider{LayerMask.LayerToName(other.gameObject.layer)}",_thisCollider.gameObject);
            }
        }
    }
}