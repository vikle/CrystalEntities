using System;
using System.Runtime.CompilerServices;

namespace CrystalEntities
{
    public readonly struct EntityView
    {
        readonly IContext m_context;

        public int Id
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]get;
        }

        public static EntityView Empty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new EntityView(null, -1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EntityView CreateEntity(IContext context)
        {
            return new EntityView(context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EntityView CreateView(IContext context, int entity)
        {
            return new EntityView(context, entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EntityView(IContext context) : this(context, context.CreateEntity())
        {
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private EntityView(IContext context, int entity)
        {
            m_context = context;
            Id = entity;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DestroyEntity()
        {
            m_context.DestroyEntity(Id);
        }

        public bool IsValidAndAlive
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (IsValid && IsAlive);
        }
        
        public bool IsValid
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Id != -1);
        }
        
        public bool IsAlive
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => m_context.IsAlive(Id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Trigger<T>() where T : struct, IEvent
        {
            return ref m_context.Trigger<T>(Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Begin<T>() where T : struct, IRequest
        {
            return ref m_context.Begin<T>(Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void End<T>() where T : struct, IRequest
        {
            m_context.End<T>(Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Has<T>() where T : struct, IFragment
        {
            return m_context.HasFragment<T>(Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<T>(ref T data) where T : unmanaged, IData
        {
            Add<ValueRef<T>>() = new ValueRef<T>(ref data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add<T>(T obj) where T : class
        {
            Add<ObjectRef<T>>() = new ObjectRef<T>(obj);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Add<T>() where T : struct, IFragment
        {
            return ref m_context.AddFragment<T>(Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Get<T>() where T : struct, IFragment
        {
            return ref m_context.GetFragment<T>(Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose<T>() where T : struct, IFragment, IDisposable
        {
            m_context.DisposeFragment<T>(Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Remove<T>() where T : struct, IFragment
        {
            m_context.RemoveFragment<T>(Id);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(EntityView a, EntityView b)
        {
            return (a.Id == b.Id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(EntityView a, EntityView b)
        {
            return (a.Id != b.Id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(EntityView other)
        {
            return (Id == other.Id);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object obj)
        {
            return obj is EntityView other && Equals(other);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return Id;
        }
    };
}
