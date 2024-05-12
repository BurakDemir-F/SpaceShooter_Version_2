using System;
using Ships;
using UnityEngine;

namespace ScoreSystem
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private ScoreUI _scoreUI;
        private PlayerShip _playerShip;
        private ScoreMultiplier _scoreMultiplier;
        public event Action<float, float> ScorePropChanged;
        public float Score => _scoreMultiplier.Score;
        public float ScoreMultiplier => _scoreMultiplier.CurrentMultiplier;
        public void Initialize(PlayerShip playerShip)
        {
            _playerShip = playerShip;
            _scoreMultiplier = GetComponent<ScoreMultiplier>();
            _scoreUI.Initialize(_scoreMultiplier);
            _playerShip.OnEnemyHit += EnemyHitHandler;
            _scoreMultiplier.ScoreChanged += ScoreChangedHandler;
            _scoreMultiplier.ScoreMultiplierChanged += ScoreMultiplierChangedHandler;
        }

        public void Destruct()
        {
            _playerShip.OnEnemyHit -= EnemyHitHandler;
            _scoreMultiplier.ScoreChanged -= ScoreChangedHandler;
            _scoreMultiplier.ScoreMultiplierChanged -= ScoreMultiplierChangedHandler;
            _scoreUI.Destruct();
        }

        private void EnemyHitHandler()
        {
            _scoreMultiplier.HandleEnemyHit();
        }

        private void ScoreChangedHandler(float newScore)
        {
            ScorePropChanged?.Invoke(_scoreMultiplier.Score,_scoreMultiplier.CurrentMultiplier);
        }
        
        private void ScoreMultiplierChangedHandler(float newMultiplier)
        {
            ScorePropChanged?.Invoke(_scoreMultiplier.Score,_scoreMultiplier.CurrentMultiplier);
        }
    }
}