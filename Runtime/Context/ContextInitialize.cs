#if UNITY_EDITOR
#define CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_PROFILING
#endif
#if !CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_RELEASE
#endif

using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace CrystalEntities
{
    partial class Context
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Init()
        {
            MakeComposites();
            CastSystems();
            InjectDependencies();
        }

        private void MakeComposites()
        {
            int composites_count = m_compositesCache.Count;
            
            for (int i = 0; i < composites_count; i++)
            {
                m_compositesCache[i].BindSystems(this);
            }
            
            for (int i = 0; i < composites_count; i++)
            {
                m_compositesCache[i].BindRequests(this);
            }
            
            for (int i = 0; i < composites_count; i++)
            {
                m_compositesCache[i].BindEvents(this);
            }
            
            m_compositesCache.Clear();
            m_compositesCache = null;
        }
        
        private void CastSystems()
        {
            var all_systems = m_systemsCache.ToArray();
            
            m_allSystems = all_systems;
            ArrayEx.WhereCast(all_systems, out m_updateSystems);
            ArrayEx.WhereCast(all_systems, out m_fixedUpdateSystems);
            ArrayEx.WhereCast(all_systems, out m_lateUpdateSystems);
            
            m_systemsCache.Clear();
            m_systemsCache = null;

#if CRYSTAL_ENTITIES_PROFILING
            m_fixedUpdateSystemsNames = m_fixedUpdateSystems.Select(s => s.GetType().Name).ToArray();
            m_updateSystemsNames = m_updateSystems.Select(s => s.GetType().Name).ToArray();
            m_lateUpdateSystemsNames = m_lateUpdateSystems.Select(s => s.GetType().Name).ToArray();
#endif
        }

        private void InjectDependencies()
        {
            var pool_type = typeof(IPool);
            const BindingFlags k_binding_flags = (BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            for (int i = 0, i_max = m_allSystems.Length; i < i_max; i++)
            {
                var system = m_allSystems[i];
                var system_type = system.GetType();
                var system_fields = system_type.GetFields(k_binding_flags);
                
                for (int j = 0, j_max = system_fields.Length; j < j_max; j++)
                {
                    var field = system_fields[j];
                    var field_type = field.FieldType;

                    if (pool_type.IsAssignableFrom(field_type))
                    {
                        var generic_pool_type = field_type.GenericTypeArguments[0];
                        var pool_instance = GetPool(generic_pool_type);
                        field.SetValue(system, pool_instance);
                        continue;
                    }

                    for (int k = 0, k_max = m_injectionsCache.Count; k < k_max; k++)
                    {
                        object injection = m_injectionsCache[k];
                        if (field_type.IsInstanceOfType(injection) == false) continue;
                        field.SetValue(system, injection);
                        break;
                    }
                }
            }
            
            m_injectionsCache.Clear();
            m_injectionsCache = null;
        }
    };
}
