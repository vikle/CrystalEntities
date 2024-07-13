#if UNITY_EDITOR
#define CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_PROFILING
#endif
#if !CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_RELEASE
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public sealed partial class Context : IContext, IContextBinding, IContextRuntime
    {
        bool[] m_entities;
        int m_entitiesCount;

        readonly Type m_poolType;
        readonly Dictionary<Type, IPool> m_poolsMap;
        IPool[] m_allPools;

        ISystem[] m_allSystems;
        IFixedUpdateSystem[] m_fixedUpdateSystems;
        IUpdateSystem[] m_updateSystems;
        ILateUpdateSystem[] m_lateUpdateSystems;

        List<IComposite> m_compositesCache;
        List<ISystem> m_systemsCache;
        ArrayList m_injectionsCache;

#if CRYSTAL_ENTITIES_PROFILING
        string[] m_fixedUpdateSystemsNames;
        string[] m_updateSystemsNames;
        string[] m_lateUpdateSystemsNames;
#endif
        
        public IReadOnlyList<bool> Entities
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => m_entities;
        }

        public int EntitiesCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => m_entitiesCount;
        }

        public Context(Type poolType)
        {
            m_entities = new bool[8];
            m_poolsMap = new Dictionary<Type, IPool>(64);
            
            m_compositesCache = new List<IComposite>(32);
            m_systemsCache = new List<ISystem>(128);
            m_injectionsCache = new ArrayList(32);

            m_poolType = poolType;
        }
        
        public int CreateEntity()
        {
            int current_entities_count = m_entitiesCount;
            
            for (int id = 0; id < current_entities_count; id++)
            {
                if (m_entities[id]) continue;
                m_entities[id] = true;
                return id;
            }

            int new_entities_count = ++m_entitiesCount;
            ArrayEx.EnsureCapacity(ref m_entities, new_entities_count);
            
            m_entities[current_entities_count] = true;

            for (int i = 0, i_max = m_allPools.Length; i < i_max; i++)
            {
                m_allPools[i].EnsureCapacity(new_entities_count);
            }
            
            return current_entities_count;
        }

        public void DestroyEntity(int entity)
        {
            m_entities[entity] = false;
            
            for (int i = 0, i_max = m_allPools.Length; i < i_max; i++)
            {
                m_allPools[i].Remove(entity);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsAlive(int entity)
        {
            return m_entities[entity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Trigger<T>(int entity) where T : struct, IEvent
        {
            return ref AddFragment<T>(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T Begin<T>(int entity) where T : struct, IRequest
        {
            return ref AddFragment<T>(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void End<T>(int entity) where T : struct, IRequest
        {
            ref var frag_ref = ref GetFragment<T>(entity);
            frag_ref.IsCompleted = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasFragment<T>(int entity) where T : struct, IFragment
        {
            var pool = GetPool<T>();
            return pool.Contains(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T AddFragment<T>(int entity) where T : struct, IFragment
        {
            var pool = GetPool<T>();
            return ref pool.Add(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref T GetFragment<T>(int entity) where T : struct, IFragment
        {
            var pool = GetPool<T>();
            return ref pool[entity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void DisposeFragment<T>(int entity) where T : struct, IFragment, IDisposable
        {
            ref var frag_ref = ref GetFragment<T>(entity);
            frag_ref.Dispose();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveFragment<T>(int entity) where T : struct, IFragment
        {
            var pool = GetPool<T>();
            pool.Remove(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IPool<T> GetPool<T>() where T : struct, IFragment
        {
            var type = typeof(T);
            var pool = GetPool(type);
            return (IPool<T>)pool;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IPool GetPool(Type type)
        {
            if (m_poolsMap.TryGetValue(type, out var pool)) return pool;

            var generic_type = m_poolType.MakeGenericType(type);
            pool = (IPool)Activator.CreateInstance(generic_type, this, m_entities.Length);
            m_poolsMap[type] = pool;

            m_allPools = m_poolsMap.Values.ToArray();
            
            return pool;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ContextEnumerator GetEnumerator()
        {
            return new ContextEnumerator(this);
        }
    };
}
