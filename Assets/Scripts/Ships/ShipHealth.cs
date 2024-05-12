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
            if (_health < 0)
            {
                _health = 0;
                OnHealthChanged?.Invoke(_health);
                OnExplode?.Invoke();
                return;
            }
            OnHealthChanged?.Invoke(_health);
        }

        public void RefreshHealth()
        {
            _health = _maxHealth;
        }
    }
}