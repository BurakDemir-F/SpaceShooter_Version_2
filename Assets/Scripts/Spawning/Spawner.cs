using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Border;
using Movement;
using Ships;
using Ships.Weapons;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Spawning
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private Pool _pool;
        [SerializeField] private BorderProvider _borderProvider;
        [FormerlySerializedAs("ShipSpawnPositions")] [SerializeField] private List<ShipSpawnProperties> _shipSpawnProperties;
        [SerializeField] private float _spawnDelay = 1f;
        
        [Header("Player")]
        [SerializeField] private string _playerPoolKey;
        [SerializeField] private Transform _playerSpawnTransform;

        [SerializeField] private PlayerTargetProvider _playerTargetProvider;
        [SerializeField] private EnemyTargetProvider _enemyTargetProvider;

        private Dictionary<ShipType, int> _spawnedShipDict;
        
        private bool _shouldSpawn;

        public void Initialize()
        {
            _pool.Initialize();
            _spawnedShipDict = new Dictionary<ShipType, int>();
        }
        public SpaceShip SpawnPlayer()
        {
            var playerShip = GetShip(_playerPoolKey, _playerSpawnTransform.position);
            playerShip.Construct(_borderProvider,_enemyTargetProvider,_pool);
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
                ShipSpawnProperties randomShipProperty;
                do
                {
                    randomShipProperty = _shipSpawnProperties[Random.Range(0, _shipSpawnProperties.Count)];
                }
                while (!CanSpawnShip(randomShipProperty.ShipType)) ;
                
                var shipKey = randomShipProperty.Key;
                var randomTransform = randomShipProperty.SpawnPositions[Random.Range(0, randomShipProperty.SpawnPositions.Count)];
                var ship = GetShip(shipKey, randomTransform.position);
                ship.OnExplode += ShipExplodeHandler;
                ship.Construct(_borderProvider,randomShipProperty.TargetProvider,_pool);
                ship.StartMoving();
                UpdateSpawnedShips(randomShipProperty.ShipType);
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

        private void ShipExplodeHandler(SpaceShip ship, WeaponType _)
        {
            ship.OnExplode -= ShipExplodeHandler;
            ship.Destruct();
            ship.GameObject.SetActive(false);
            _pool.Return(ship);
        }

        private bool CanSpawnShip(ShipType shipType)
        {
            if (_spawnedShipDict.TryGetValue(shipType, out var currentSpawnedAmount))
            {
                var shipProp= _shipSpawnProperties.First(ship => ship.ShipType == shipType);
                if (shipProp.ShipType == ShipType.None)
                    return false;

                var spawnLimit = shipProp.spawnLimit;
                return currentSpawnedAmount < spawnLimit;
            }

            return true;
        }

        private void UpdateSpawnedShips(ShipType shipType)
        {
            if (_spawnedShipDict.TryGetValue(shipType, out var spawnedAmount))
            {
                _spawnedShipDict[shipType] += 1;
            }
            else
            {
                _spawnedShipDict.Add(shipType,1);
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            if(_shipSpawnProperties == null && _shipSpawnProperties.Count == 0)
                return;

            Gizmos.color = Color.yellow;
            foreach (var shipSpawnProperties in _shipSpawnProperties)
            {
                foreach (var spawnTransform in shipSpawnProperties.SpawnPositions)
                {
                    Gizmos.DrawSphere(spawnTransform.position,3f);
                }
            }
        }

        [System.Serializable]
        private class ShipSpawnProperties
        {
            public string Key;
            public ShipType ShipType;
            public List<Transform> SpawnPositions;
            public TargetProvider TargetProvider;
            public int spawnLimit = int.MaxValue;
        }
    }
}