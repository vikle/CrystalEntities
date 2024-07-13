using System;
using System.Runtime.CompilerServices;
using UnityEngine;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

namespace CrystalEntities
{
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    [DefaultExecutionOrder(-100)]
    public sealed class ECSEngine : MonoBehaviour
    {
        public ECSBootstrap bootstrap;

        public static IContext Context
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]get;
            private set;
        }

        static IContextRuntime s_runtime;

        void Awake()
        {
            var context = new Context(typeof(SparsePool<>));

            Context = context;
            s_runtime = context;

            if (bootstrap != null)
            {
                bootstrap.OnStartBootstrap(context);
            }

            context.Init();
        }

        void Start()
        {
            s_runtime.RunStart();
        }

        void Update()
        {
            TimeData.OnUpdate();
            s_runtime.RunUpdate();
        }

        void FixedUpdate()
        {
            TimeData.OnFixedUpdate();
            s_runtime.RunFixedUpdate();
        }

        void LateUpdate()
        {
            s_runtime.RunLateUpdate();
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Tools/Crystal Entities/Create/Engine", true)]
        private static bool CanCreateEngine()
        {
            return (FindObjectOfType<ECSEngine>() == null);
        }

        [UnityEditor.MenuItem("Tools/Crystal Entities/Create/Engine")]
        private static void CreateEngine()
        {
            var obj_type = typeof(ECSEngine);
            CreateGameObject(obj_type);
        }

        [UnityEditor.MenuItem("Tools/Crystal Entities/Create/Bootstrap", true)]
        private static bool CanCreateBootstrap()
        {
            return (FindObjectOfType<ECSBootstrap>() == null);
        }

        [UnityEditor.MenuItem("Tools/Crystal Entities/Create/Bootstrap")]
        private static void CreateBootstrap()
        {
            var obj_type = typeof(ECSBootstrap);
            CreateGameObject(obj_type);
        }

        private static void CreateGameObject(Type objType)
        {
            var main_obj = new GameObject(objType.Name, objType)
            {
                isStatic = true
            };
            
            UnityEditor.Selection.activeObject = main_obj;
        }
#endif
    };
}
