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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RunAwake()
        {
            for (int i = 0, i_max = m_allSystems.Length; i < i_max; i++)
            {
                if (m_allSystems[i] is IAwakeSystem system)
                {
                    RunOnAwake(system);
                    RunOnAwake_Debug(system);
                }
            }
        }

        [Conditional("CRYSTAL_ENTITIES_RELEASE"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void RunOnAwake(IAwakeSystem system)
        {
            ProfilerAPI.BeginSample(system.GetType().Name);
            system.OnAwake();
            ProfilerAPI.EndSample();
        }
        
        [Conditional("CRYSTAL_ENTITIES_DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void RunOnAwake_Debug(IAwakeSystem system)
        {
            try
            {
                ProfilerAPI.BeginSample(system.GetType().Name);
                system.OnAwake();
                ProfilerAPI.EndSample();
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RunStart()
        {
            for (int i = 0, i_max = m_allSystems.Length; i < i_max; i++)
            {
                if (m_allSystems[i] is IStartSystem system)
                {
                    RunOnStart(system);
                    CallStart_Debug(system);
                }
            }
        }
        
        [Conditional("CRYSTAL_ENTITIES_RELEASE"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void RunOnStart(IStartSystem system)
        {
            ProfilerAPI.BeginSample(system.GetType().Name);
            system.OnStart();
            ProfilerAPI.EndSample();
        }
        
        [Conditional("CRYSTAL_ENTITIES_DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CallStart_Debug(IStartSystem system)
        {
            try
            {
                ProfilerAPI.BeginSample(system.GetType().Name);
                system.OnStart();
                ProfilerAPI.EndSample();
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }
        
        
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
            m_fixedUpdateSystems[index].OnFixedUpdate();
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
                m_fixedUpdateSystems[index].OnFixedUpdate();
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.EndSample();
#endif
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }
        
        
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
            m_updateSystems[index].OnUpdate();
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
                m_updateSystems[index].OnUpdate();
#if CRYSTAL_ENTITIES_PROFILING
                ProfilerAPI.EndSample();
#endif
            }
            catch (Exception e)
            {
                LoggerAPI.LogException(e);
            }
        }
        
        
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
            m_lateUpdateSystems[index].OnLateUpdate();
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
                m_lateUpdateSystems[index].OnLateUpdate();
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
