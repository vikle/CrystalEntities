using System.Runtime.CompilerServices;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

namespace CrystalEntities
{
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public sealed class SparsePool<T> : IPool<T> where T : struct, IFragment
    {
        T[] m_dense;
        int m_denseCapacity;
        int[] m_sparse;
        int[] m_recycledStack;
        int m_recycledCount;

        readonly IContext m_context;
        
        public ref T this[int entity]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref m_dense[m_sparse[entity]];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparsePool(IContext context, int capacity)
        {
            m_context = context;

            m_denseCapacity = 1;
            m_dense = new T[m_denseCapacity];
            m_sparse = new int[capacity];
            m_recycledStack = new int[m_denseCapacity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int capacity)
        {
            ArrayEx.EnsureCapacity(ref m_sparse, capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(int entity)
        {
            return (m_sparse[entity] > 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Add(int entity)
        {
            int pointer;
            
            if (m_recycledCount > 0)
            {
                pointer = m_recycledStack[--m_recycledCount];
            }
            else
            {
                pointer = m_denseCapacity;
                ArrayEx.EnsureCapacity(ref m_dense, ++m_denseCapacity);
            }
            
            m_sparse[entity] = pointer;
            
            return ref m_dense[pointer];
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove(int entity)
        {
            ref int pointer = ref m_sparse[entity];
            if (pointer == 0) return;
            
            ArrayEx.EnsureCapacity(ref m_recycledStack, m_recycledCount + 1);
            m_recycledStack[m_recycledCount++] = pointer;
            m_dense[pointer] = default;
            pointer = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PoolEnumerator GetEnumerator()
        {
            return new PoolEnumerator(this, m_context);
        }
    };
}
