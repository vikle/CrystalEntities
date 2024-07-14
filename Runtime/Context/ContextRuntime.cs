#if UNITY_EDITOR
#define CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_PROFILING
#endif
#if !CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_RELEASE
#endif

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CrystalEntities
{
    using Profiling;
    using Logging;

    partial class Context
    {
        //IStartSystem
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RunStart()
        {
            for (int i = 0, i_max = m_allSystems.Length; i < i_max; i++)
            {
                if (m_allSystems[i] is IStartSystem system)
                {
                    RunOnStart(system);
                    RunOnStart_Debug(system);
                }
            }
        }

        [Conditional("CRYSTAL_ENTITIES_RELEASE"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnStart(IStartSystem system)
        {
            ProfilerAPI.BeginSample(system.GetType().Name);
            system.OnStart(this);
            ProfilerAPI.EndSample();
        }

        [Conditional("CRYSTAL_ENTITIES_DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnStart_Debug(IStartSystem system)
        {
            try
            {
                ProfilerAPI.BeginSample(system.GetType().Name);
                system.OnStart(this);
                ProfilerAPI.EndSample();
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }

        //IFixedUpdateSystem
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RunFixedUpdate()
        {
            for (int i = 0, i_max = m_fixedUpdateSystems.Length; i < i_max; i++)
            {
                RunOnFixedUpdate(i);
                RunOnFixedUpdate_Debug(i);
            }
        }

        [Conditional("CRYSTAL_ENTITIES_RELEASE"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnFixedUpdate(int index)
        {
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.BeginSample(m_fixedUpdateSystemsNames[index]);
#endif
            m_fixedUpdateSystems[index].OnFixedUpdate(this);
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.EndSample();
#endif
        }

        [Conditional("CRYSTAL_ENTITIES_DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnFixedUpdate_Debug(int index)
        {
            try
            {
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.BeginSample(m_fixedUpdateSystemsNames[index]);
#endif
                m_fixedUpdateSystems[index].OnFixedUpdate(this);
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.EndSample();
#endif
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }

        //IUpdateSystem
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RunUpdate()
        {
            for (int i = 0, i_max = m_updateSystems.Length; i < i_max; i++)
            {
                RunOnUpdate(i);
                RunOnUpdate_Debug(i);
            }
        }

        [Conditional("CRYSTAL_ENTITIES_RELEASE"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnUpdate(int index)
        {
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.BeginSample(m_updateSystemsNames[index]);
#endif
            m_updateSystems[index].OnUpdate(this);
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.EndSample();
#endif
        }

        [Conditional("CRYSTAL_ENTITIES_DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnUpdate_Debug(int index)
        {
            try
            {
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.BeginSample(m_updateSystemsNames[index]);
#endif
                m_updateSystems[index].OnUpdate(this);
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.EndSample();
#endif
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }

        //ILateUpdateSystem
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RunLateUpdate()
        {
            for (int i = 0, i_max = m_lateUpdateSystems.Length; i < i_max; i++)
            {
                RunOnLateUpdate(i);
                RunOnLateUpdate_Debug(i);
            }
        }

        [Conditional("CRYSTAL_ENTITIES_RELEASE"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnLateUpdate(int index)
        {
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.BeginSample(m_lateUpdateSystemsNames[index]);
#endif
            m_lateUpdateSystems[index].OnLateUpdate(this);
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.EndSample();
#endif
        }

        [Conditional("CRYSTAL_ENTITIES_DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnLateUpdate_Debug(int index)
        {
            try
            {
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.BeginSample(m_lateUpdateSystemsNames[index]);
#endif
                m_lateUpdateSystems[index].OnLateUpdate(this);
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.EndSample();
#endif
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }

        //IEntityInitializeSystem
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunAfterEntityCreated(int entity)
        {
            for (int i = 0, i_max = m_entityInitializeSystems.Length; i < i_max; i++)
            {
                RunOnRunAfterEntityCreated(i, entity);
                RunOnRunAfterEntityCreated_Debug(i, entity);
            }
        }

        [Conditional("CRYSTAL_ENTITIES_RELEASE"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnRunAfterEntityCreated(int index, int entity)
        {
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.BeginSample(m_entityInitializeSystemsNames[index]);
#endif
            m_entityInitializeSystems[index].OnAfterEntityCreated(this, entity);
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.EndSample();
#endif
        }

        [Conditional("CRYSTAL_ENTITIES_DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnRunAfterEntityCreated_Debug(int index, int entity)
        {
            try
            {
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.BeginSample(m_entityInitializeSystemsNames[index]);
#endif
                m_entityInitializeSystems[index].OnAfterEntityCreated(this, entity);
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.EndSample();
#endif
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }

        //IEntityTerminateSystem
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunBeforeEntityDestroyed(int entity)
        {
            for (int i = 0, i_max = m_entityTerminateSystems.Length; i < i_max; i++)
            {
                RunOnBeforeEntityDestroyed(i, entity);
                RunOnBeforeEntityDestroyed_Debug(i, entity);
            }
        }

        [Conditional("CRYSTAL_ENTITIES_RELEASE"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnBeforeEntityDestroyed(int index, int entity)
        {
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.BeginSample(m_entityTerminateSystemsNames[index]);
#endif
            m_entityTerminateSystems[index].OnBeforeEntityDestroyed(this, entity);
#if CRYSTAL_ENTITIES_PROFILING
            ProfilerAPI.EndSample();
#endif
        }

        [Conditional("CRYSTAL_ENTITIES_DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void RunOnBeforeEntityDestroyed_Debug(int index, int entity)
        {
            try
            {
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.BeginSample(m_entityTerminateSystemsNames[index]);
#endif
                m_entityTerminateSystems[index].OnBeforeEntityDestroyed(this, entity);
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.EndSample();
#endif
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }
    };
}
