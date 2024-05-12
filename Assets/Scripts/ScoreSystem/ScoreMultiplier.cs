using System;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreMultiplier : MonoBehaviour
    {
        [SerializeField] private float _multiplyDuration = 2f;
        [SerializeField] private float _currentMultiplier;
        [SerializeField] private float _currentScore;
        [SerializeField] private float _singleShipHitScore = 10f;

        public event Action<float> ScoreChanged;
        public event Action<float> ScoreMultiplierChanged; 

        public float Score => _currentScore;
        public float CurrentMultiplier => _currentMultiplier;

        public bool IsEnemyHitInSec() => _isEnemyHitInSec;
        
        private bool _isEnemyHitInSec;
        private float _afterHitCounter;
        public void HandleEnemyHit()
        {
            if (!_isEnemyHitInSec)
            {
                _isEnemyHitInSec = true;
                _afterHitCounter = 0f;
                UpdateScore(_singleShipHitScore);
                return;
            }
            
            if (_isEnemyHitInSec && _afterHitCounter < _multiplyDuration)
            {
                UpdateMultiplier();
                UpdateScore(_singleShipHitScore);
                _afterHitCounter = 0f;
            }
        }

        private void UpdateMultiplier()
        {
            _currentMultiplier++;
            ScoreMultiplierChanged?.Invoke(_currentMultiplier);
        }

        private void UpdateScore(float scoreToAdd)
        {
            _currentScore += scoreToAdd * _currentMultiplier;
            ScoreChanged?.Invoke(_currentScore);
        }

        private void Update()
        {
            if (_isEnemyHitInSec)
            {
                _afterHitCounter += Time.deltaTime;
                if (_afterHitCounter > _multiplyDuration)
                    _isEnemyHitInSec = false;
            }
        }

        public float GetNormalizedHitCounter()
        {
            return (_multiplyDuration - _afterHitCounter) / _multiplyDuration;
        }
    }
}