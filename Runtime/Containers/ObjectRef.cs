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
    public readonly struct ObjectRef<T> : IComponent where T : class
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ObjectRef(T target)
        {
            Target = target;
        }

        public bool IsValid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Target != null);
        }
    
        public T Target
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]get;
        }
    };
}