using System;
using ScoreSystem;
using Ships;
using Spawning;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private HealthUI _healthUI;
        [SerializeField] private ScoreManager _scoreManager;

        private void Start()
        {
            _spawner.Initialize();
            var playerShip = _spawner.SpawnPlayer();
            _healthUI.SetHealth(playerShip.ShipHealth);
            _spawner.StartSpawning();
            
            _scoreManager.Initialize(playerShip as PlayerShip);
        }

        private void OnDestroy()
        {
            _scoreManager.Destruct();
        }
    }
}