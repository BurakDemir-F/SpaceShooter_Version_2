namespace Spawning
{
    public interface IPool
    {
        T Get<T>(string key) where T : IPoolObject;
        void Return(IPoolObject poolObject);
    }
}