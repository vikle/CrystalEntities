using System.Runtime.CompilerServices;

namespace CrystalEntities
{
    public struct PoolEnumerator
    {
        readonly IPool m_pool;
        ContextEnumerator m_contextEnumerator;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public PoolEnumerator(IPool pool, IContext context)
        {
            m_pool = pool;
            m_contextEnumerator = context.GetEnumerator();
        }

        public int Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => m_contextEnumerator.Current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            var pool = m_pool;
            ref var enumerator = ref m_contextEnumerator;
            
            do
            {
                if (enumerator.MoveNext() == false) return false;
            } while (pool.Contains(enumerator.Current) == false);

            return true;
        }
    };
}
