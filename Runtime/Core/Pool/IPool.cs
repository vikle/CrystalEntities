namespace CrystalEntities
{
    public interface IPool
    {
        void EnsureCapacity(int capacity);
        bool Contains(int entity);
        void Remove(int entity);
        PoolEnumerator GetEnumerator();
    };

    public interface IPool<T> : IPool where T : struct, IFragment
    {
        ref T this[int entity] { get; }
        ref T Add(int entity);
    };
}
