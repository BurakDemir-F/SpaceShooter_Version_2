using System;
using UnityEngine;

namespace Ships
{
    public class ShipHealth : MonoBehaviour , IDamageable
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _health;
        public int MaxHealth => _maxHealth;
        public int CurrentHealth => _health;
        
        public event Action OnExplode;
        public event Action<int> OnHealthChanged; 
        public void Damage(int damage)
        {
            _health -= damage;
            OnHealthChanged?.Invoke(_health);
            if (_health < 0)
            {
                _health = 0;
                OnHealthChanged?.Invoke(_health);
                OnExplode?.Invoke();
            }
        }

        public void RefreshHealth()
        {
            _health = _maxHealth;
        }
    }
}