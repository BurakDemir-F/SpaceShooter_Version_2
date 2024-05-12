using System;
using UnityEngine;

namespace Ships
{
    public class ShipHealth : MonoBehaviour , IDamageable
    {
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _health;
        public float MaxHealth => _maxHealth;
        public float CurrentHealth => _health;
        public float HealthNormalized => _health / _maxHealth;
        
        public event Action OnExplode;
        public event Action<float> OnHealthChanged; 
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