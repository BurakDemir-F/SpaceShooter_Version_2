using System.Collections.Generic;
using UnityEngine;

namespace Spawning
{
    public class Pool : MonoBehaviour, IPool
    {
        [SerializeField] private List<PoolProperty> _poolProperties;
        [SerializeField] private Transform _poolRoot;
        
        private Dictionary<string, Stack<IPoolObject>> _poolDict;
        private Dictionary<string, PoolProperty> _keyPropertyDict;
        public void Initialize()
        {
            _poolDict = new Dictionary<string, Stack<IPoolObject>>(_poolProperties.Count);
            _keyPropertyDict = new Dictionary<string, PoolProperty>(_poolProperties.Count);
            
            foreach (var poolProperty in _poolProperties)
            {
                _keyPropertyDict.Add(poolProperty.Key,poolProperty);
            }

            foreach (var poolProperty in _poolProperties)
            {
                var stack = new Stack<IPoolObject>();
                _poolDict.Add(poolProperty.Key,stack);
                for (int i = 0; i < poolProperty.InitialAmount; i++)
                {
                    stack.Push(CreateNewObject(poolProperty));
                }
            }
        }
        
        public T Get<T>(string key) where T : IPoolObject
        {
            if (!_poolDict.ContainsKey(key))
                return default;

            var stack = _poolDict[key];
            if (stack.Count > 0)
            {
                var poolObj = stack.Pop();
                poolObj.OnGetFromPool();
                return (T)poolObj;
            }

            var newObj = (T)CreateNewObject(_keyPropertyDict[key]);
            newObj.GameObject.transform.parent = null;
            newObj.OnGetFromPool();
            return newObj;
        }

        public void Return(IPoolObject poolObject)
        {
            var key = poolObject.Key;
            if (!_poolDict.ContainsKey(key))
            {
                Debug.Log("something wrong here!");
                return;
            }
            
            poolObject.GameObject.transform.SetParent(_poolRoot);
            var stack = _poolDict[key];
            stack.Push(poolObject);
            poolObject.OnReturnToPool();
        }

        private IPoolObject CreateNewObject(PoolProperty poolProperty)
        {
            var newObj = Instantiate(poolProperty.Prefab, _poolRoot);
            newObj.gameObject.SetActive(false);
            var poolObj =  newObj.GetComponent<IPoolObject>();
            poolObj.Pool = this;
            poolObj.Key = poolProperty.Key;
            poolObj.GameObject = newObj;
            return poolObj;
        }
        
        [System.Serializable]
        private class PoolProperty
        {
            public string Key;
            public GameObject Prefab;
            public int InitialAmount;
        }
    }

}