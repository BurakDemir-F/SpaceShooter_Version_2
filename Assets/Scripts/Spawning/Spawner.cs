using System;
using System.Collections;
using System.Collections.Generic;
using Border;
using Movement;
using Ships;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawning
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Pool _pool;
        [SerializeField] private BorderProvider _borderProvider;
        [SerializeField] private List<ShipSpawnProperties> ShipSpawnPositions;
        [SerializeField] private float _spawnDelay = 1f;
        
        [Header("Player")]
        [SerializeField] private string _playerPoolKey;
        [SerializeField] private Transform _playerSpawnTransform;

        [SerializeField] private PlayerTargetProvider _playerTargetProvider;
        
        private bool _shouldSpawn;

        public void InitializePool()
        {
            _pool.Initialize();
        }
        public SpaceShip SpawnPlayer()
        {
            var playerShip = GetShip(_playerPoolKey, _playerSpawnTransform.position);
            playerShip.Construct(_borderProvider,null,_pool);
            playerShip.StartMoving();
            _playerTargetProvider.SetPlayerTransform(playerShip.transform);
            return playerShip;
        }
        
        public void StartSpawning()
        {
            _shouldSpawn = true;
            StartCoroutine(SpawnCor());
        }

        private IEnumerator SpawnCor()
        {
            var wait = new WaitForSeconds(_spawnDelay);
            while (_shouldSpawn)
            {
                var randomShipProperty = ShipSpawnPositions[Random.Range(0, ShipSpawnPositions.Count)];
                var shipKey = randomShipProperty.Key;
                var randomTransform = randomShipProperty.SpawnPositions[Random.Range(0, randomShipProperty.SpawnPositions.Count)];
                var ship = GetShip(shipKey, randomTransform.position);
                ship.OnExplode += ShipExplodeHandler;
                ship.Construct(_borderProvider,randomShipProperty.TargetProvider,_pool);
                ship.StartMoving();
                yield return wait;
            }
        }

        private SpaceShip GetShip(string shipKey, Vector3 position)
        {
            var ship = _pool.Get<SpaceShip>(shipKey);
            ship.transform.position = position;
            ship.transform.SetParent(transform);

            return ship;
        }

        private void ShipExplodeHandler(SpaceShip ship)
        {
            ship.OnExplode -= ShipExplodeHandler;
            ship.Destruct();
            ship.GameObject.SetActive(false);
            _pool.Return(ship);
        }
        
        private void OnDrawGizmosSelected()
        {
            if(ShipSpawnPositions == null && ShipSpawnPositions.Count == 0)
                return;

            Gizmos.color = Color.black;
            foreach (var shipSpawnProperties in ShipSpawnPositions)
            {
                foreach (var spawnTransform in shipSpawnProperties.SpawnPositions)
                {
                    Gizmos.DrawSphere(spawnTransform.position,1f);
                }
            }
        }

        [System.Serializable]
        private class ShipSpawnProperties
        {
            public string Key;
            public List<Transform> SpawnPositions;
            public TargetProvider TargetProvider;
        }
    }
}