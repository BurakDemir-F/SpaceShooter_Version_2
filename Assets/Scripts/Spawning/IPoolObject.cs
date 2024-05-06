using UnityEngine;

namespace Spawning
{
    public interface IPoolObject
    {
        string Key { get; set; }
        IPool Pool { get; set; }
        GameObject GameObject { get; set; }
        void OnGetFromPool();
        void OnReturnToPool();
    }
}