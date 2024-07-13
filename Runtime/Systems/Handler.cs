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
    public abstract class Handler<T> 
        where T : struct, IFragment
    {
        protected readonly IPool<T> m_data1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void OnFixedUpdate(IContext context)
        {
            UpdateInternal(context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void OnUpdate(IContext context)
        {
            UpdateInternal(context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual void OnLateUpdate(IContext context)
        {
            UpdateInternal(context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdateInternal(IContext context)
        {
            foreach (int entity in context)
            {
                if (IsCanHandle(entity))
                {
                    OnHandle(context, entity);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual bool IsCanHandle(int entity)
        {
            return m_data1.Contains(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void OnHandle(IContext context, int entity);
    };
    
    public abstract class Handler<T1, T2> : Handler<T1>
        where T1 : struct, IFragment
        where T2 : struct, IFragment
    {
        protected readonly IPool<T2> m_data2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool IsCanHandle(int entity)
        {
            return base.IsCanHandle(entity) 
                && m_data2.Contains(entity);
        }
    };
    
    public abstract class Handler<T1, T2, T3> : Handler<T1, T2>
        where T1 : struct, IFragment
        where T2 : struct, IFragment
        where T3 : struct, IFragment
    {
        protected readonly IPool<T3> m_data3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool IsCanHandle(int entity)
        {
            return base.IsCanHandle(entity) 
                && m_data3.Contains(entity);
        }
    };
    
    public abstract class Handler<T1, T2, T3, T4> : Handler<T1, T2, T3>
        where T1 : struct, IFragment
        where T2 : struct, IFragment
        where T3 : struct, IFragment
        where T4 : struct, IFragment
    {
        protected readonly IPool<T4> m_data4;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool IsCanHandle(int entity)
        {
            return base.IsCanHandle(entity)
                && m_data4.Contains(entity);
        }
    };
    
    public abstract class Handler<T1, T2, T3, T4, T5> : Handler<T1, T2, T3, T4>
        where T1 : struct, IFragment
        where T2 : struct, IFragment
        where T3 : struct, IFragment
        where T4 : struct, IFragment
        where T5 : struct, IFragment
    {
        protected readonly IPool<T5> m_data5;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool IsCanHandle(int entity)
        {
            return base.IsCanHandle(entity) 
                && m_data5.Contains(entity);
        }
    };
    
    public abstract class Handler<T1, T2, T3, T4, T5, T6> : Handler<T1, T2, T3, T4, T5>
        where T1 : struct, IFragment
        where T2 : struct, IFragment
        where T3 : struct, IFragment
        where T4 : struct, IFragment
        where T5 : struct, IFragment
        where T6 : struct, IFragment
    {
        protected readonly IPool<T6> m_data6;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool IsCanHandle(int entity)
        {
            return base.IsCanHandle(entity) 
                && m_data6.Contains(entity);
        }
    };
    
    public abstract class Handler<T1, T2, T3, T4, T5, T6, T7> : Handler<T1, T2, T3, T4, T5, T6>
        where T1 : struct, IFragment
        where T2 : struct, IFragment
        where T3 : struct, IFragment
        where T4 : struct, IFragment
        where T5 : struct, IFragment
        where T6 : struct, IFragment
        where T7 : struct, IFragment
    {
        protected readonly IPool<T7> m_data7;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool IsCanHandle(int entity)
        {
            return base.IsCanHandle(entity) 
                && m_data7.Contains(entity);
        }
    };
    
    public abstract class Handler<T1, T2, T3, T4, T5, T6, T7, T8> : Handler<T1, T2, T3, T4, T5, T6, T7>
        where T1 : struct, IFragment
        where T2 : struct, IFragment
        where T3 : struct, IFragment
        where T4 : struct, IFragment
        where T5 : struct, IFragment
        where T6 : struct, IFragment
        where T7 : struct, IFragment
        where T8 : struct, IFragment
    {
        protected readonly IPool<T8> m_data8;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override bool IsCanHandle(int entity)
        {
            return base.IsCanHandle(entity) 
                && m_data8.Contains(entity);
        }
    };
}
