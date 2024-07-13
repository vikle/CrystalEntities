#if UNITY_EDITOR
#define CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_PROFILING
#endif
#if !CRYSTAL_ENTITIES_DEBUG
#define CRYSTAL_ENTITIES_RELEASE
#endif

using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Profiling;

namespace CrystalEntities.Profiling
{
    public static class ProfilerAPI
    {
        [Conditional("CRYSTAL_ENTITIES_PROFILING"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BeginSample(string name)
        {
            Profiler.BeginSample(name);
        }

        [Conditional("CRYSTAL_ENTITIES_PROFILING"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EndSample()
        {
            Profiler.EndSample();
        }
    };
}
