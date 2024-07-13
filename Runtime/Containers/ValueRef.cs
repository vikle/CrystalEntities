using System.Runtime.CompilerServices;

namespace CrystalEntities
{
    public readonly unsafe struct ValueRef<T> : IComponent where T : unmanaged, IData
    {
        readonly T* m_pointer;

        public bool IsValid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (m_pointer != null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ValueRef(ref T data)
        {
            fixed (T* ptr = &data)
            {
                m_pointer = ptr;
            }
        }

        public ref readonly T ValueRO
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref *m_pointer;
        }
        
        public ref T ValueRW
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref *m_pointer;
        }
    };
}
