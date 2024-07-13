#if UNITY_EDITOR
#define CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_PROFILING
#endif
#if !CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_RELEASE
#endif

using System;
using System.Diagnostics;

namespace CrystalEntities.Logging
{
    public static class LoggerAPI
    {
        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional("CRYSTAL_ENTITIES_DEBUG")]
        public static void Log(object message)
        {
            UnityEngine.Debug.Log($"[CRYSTAL_ENTITIES] {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional("CRYSTAL_ENTITIES_DEBUG")]
        public static void LogWarning(object message)
        {
            UnityEngine.Debug.LogWarning($"[CRYSTAL_ENTITIES] {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional("CRYSTAL_ENTITIES_DEBUG")]
        public static void LogError(object message)
        {
            UnityEngine.Debug.LogError($"[CRYSTAL_ENTITIES] {message}");
        }

        // ReSharper disable Unity.PerformanceAnalysis
        [Conditional("CRYSTAL_ENTITIES_DEBUG")]
        public static void LogException(Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static void DebugLog(object message)
        {
            UnityEngine.Debug.Log($"[CRYSTAL_ENTITIES] {message}");
        }
    };
}
