using System;
using Spawning;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Spawner _spawner;
        [SerializeField] private HealthUI _healthUI;

        private void Start()
        {
            _spawner.InitializePool();
            var playerShip = _spawner.SpawnPlayer();
            _healthUI.SetHealth(playerShip.ShipHealth);
            _spawner.StartSpawning();
        }
    }
}