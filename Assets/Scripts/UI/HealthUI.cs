using Ships;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HealthUI : MonoBehaviour 
    {
        [SerializeField] private TMP_Text _healthText;
        private ShipHealth _health;

        public void SetHealth(ShipHealth health)
        {
            _health = health;
            _health.OnHealthChanged += HealthChangedHandler;
            _health.OnExplode += ExplodeHandler;
            UpdateUI(health.CurrentHealth);
        }

        private void OnDestroy()
        {
            _health.OnHealthChanged -= HealthChangedHandler;
            _health.OnExplode -= ExplodeHandler;
        }

        private void HealthChangedHandler(int health)
        {
            UpdateUI(health);
        }

        private void ExplodeHandler()
        {
            _health.OnHealthChanged -= HealthChangedHandler;
            _healthText.SetText("Game Over");
        }

        private void UpdateUI(int newHealth)
        {
            _healthText.SetText($"Health: {newHealth.ToString()}");
        }
    }
}