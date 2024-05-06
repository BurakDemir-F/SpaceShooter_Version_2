using Spawning;
using UnityEngine;

namespace Ships.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform _fireTransform;
        [SerializeField] private string _bulletKey;
        [SerializeField] private Vector3 _shootDirection;
        [SerializeField] private int _damage;
        private IPool _bullet;

        public Transform FireTransform => _fireTransform;
        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
    }
}