using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CrystalEntities
{
    public struct ContextEnumerator
    {
        readonly IReadOnlyList<bool> m_entities;
        readonly int m_count;
        int m_index;
            
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ContextEnumerator(IContext context)
        {
            m_entities = context.Entities;
            m_count = context.EntitiesCount;
            m_index = -1;
        }

        public int Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => m_index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            int count = m_count;
            ref int index = ref m_index;
            var entities = m_entities;
            
            if (++index >= count) return false;
                
            while (entities[index] == false)
            {
                if (++index >= count) return false;
            }
                
            return true;
        }
    };
}
