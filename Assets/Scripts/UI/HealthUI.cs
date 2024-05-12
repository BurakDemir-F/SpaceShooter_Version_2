using Ships;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour 
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private Image _healthBar;
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

        private void HealthChangedHandler(float health)
        {
            UpdateUI(health);
        }

        private void ExplodeHandler()
        {
            _health.OnHealthChanged -= HealthChangedHandler;
            _healthText.SetText("Game Over");
        }

        private void UpdateUI(float newHealth)
        {
            _healthText.SetText($"Health: {newHealth.ToString()}");
            _healthBar.fillAmount = _health.HealthNormalized;
        }
    }
}