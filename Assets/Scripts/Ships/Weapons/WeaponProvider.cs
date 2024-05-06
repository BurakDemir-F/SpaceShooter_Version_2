using UnityEngine;

namespace Ships.Weapons
{
    public class WeaponProvider : MonoBehaviour
    {
        [SerializeField] private Weapon _weapon;
        public Weapon Get() => _weapon;
    }
}