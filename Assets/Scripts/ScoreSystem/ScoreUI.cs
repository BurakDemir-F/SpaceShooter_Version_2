using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace ScoreSystem
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _scoreMultiplierText;
        [SerializeField] private Image _multiplierSlider;

        private ScoreMultiplier _scoreMultiplier;

        public void Initialize(ScoreMultiplier multiplier)
        {
            _scoreMultiplier = multiplier;
            _scoreMultiplier.ScoreChanged += ScoreChangedHandler;
            UpdateUI(_scoreMultiplier.Score,_scoreMultiplier.CurrentMultiplier);
        }

        public void Destruct()
        {
            _scoreMultiplier.ScoreChanged -= ScoreChangedHandler;
            _scoreMultiplier.ScoreMultiplierChanged -= ScoreMultiplierChanged;
        }

        private void ScoreMultiplierChanged(float scoreMultiplier)
        {
            UpdateUI(_scoreMultiplier.Score,scoreMultiplier);
        }

        private void ScoreChangedHandler(float score)
        {
            UpdateUI(score,_scoreMultiplier.CurrentMultiplier);
        }

        private void UpdateUI(float score, float multiplier)
        {
            _scoreText.text = $"Score: {score}";
            _scoreMultiplierText.text = $"Multiplier: {multiplier}X";
        }

        private void Update()
        {
            if (!_scoreMultiplier.IsEnemyHitInSec())
            {
                if (_multiplierSlider.gameObject.activeSelf)
                {
                    _multiplierSlider.gameObject.SetActive(false);
                }
                
                return;
            }

            if (!_multiplierSlider.gameObject.activeSelf)
            {
                _multiplierSlider.gameObject.SetActive(true);
            }

            _multiplierSlider.fillAmount = _scoreMultiplier.GetNormalizedHitCounter();
        }
    }
}