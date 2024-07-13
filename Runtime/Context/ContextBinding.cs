using System.Runtime.CompilerServices;

namespace CrystalEntities
{
    partial class Context
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IContextBinding BindComposite<T>() where T : class, IComposite, new()
        {
            m_compositesCache.Add(new T());
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IContextBinding BindRequest<T>() where T : struct, IRequest
        {
            BindSystem<RequestCollector<T>>();
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IContextBinding BindEvent<T>() where T : struct, IEvent
        {
            BindSystem<EventCollector<T>>();
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IContextBinding BindSystem<T>() where T : class, ISystem, new()
        {
            m_systemsCache.Add(new T());
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IContextBinding Inject(object data)
        {
            if (m_injectionsCache.Contains(data) == false)
            {
                m_injectionsCache.Add(data);
            }
            
            return this;
        }
    };
}
