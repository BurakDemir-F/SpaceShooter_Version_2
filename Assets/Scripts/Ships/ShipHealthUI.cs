using System;
using UnityEngine;
using Utilities;

namespace Ships
{
    public class ShipHealthUI : MonoBehaviour
    {
        [SerializeField] private Transform _healthBar;
        [SerializeField] private float _visibilityDuration = 2f;
        private ShipHealth _health;
        private bool _isTakingHit;
        private float _takingHitCounter;
        public void Initialize(ShipHealth health)
        {
            _health = health;
            _health.OnHealthChanged += HealthChangedHandler;
        }

        public void Destruct()
        {
            _health.OnHealthChanged -= HealthChangedHandler;
        }

        private void HealthChangedHandler(float newHealth)
        {
            var scale = _healthBar.transform.localScale;
            _healthBar.transform.localScale = scale.SetX(_health.HealthNormalized);
            _isTakingHit = true;
            _takingHitCounter = 0f;
        }

        private void Update()
        {
            if (!_isTakingHit)
            {
                DisableHealthBar();
                _takingHitCounter = 0f;
                return;
            }

            _takingHitCounter += Time.deltaTime;
            if (_takingHitCounter < _visibilityDuration)
                EnableHealthBar();
            else
            {
                _isTakingHit = false;
            }
        }

        private void DisableHealthBar()
        {
            if (_healthBar.gameObject.activeSelf)
                _healthBar.gameObject.SetActive(false);
        }

        private void EnableHealthBar()
        {
            if (!_healthBar.gameObject.activeSelf)
                _healthBar.gameObject.SetActive(true);
        }
    }
}