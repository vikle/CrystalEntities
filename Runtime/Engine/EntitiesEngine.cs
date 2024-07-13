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
    [Il2CppEagerStaticClassConstruction]
#endif
    [DefaultExecutionOrder(-999)]
    public sealed class EntitiesEngine : MonoBehaviour
    {
        public static IContext Context { [MethodImpl(MethodImplOptions.AggressiveInlining)]get; }
        static readonly IContextBinding sr_contextBinding;
        static readonly IContextRuntime sr_contextRuntime;
        
        static EntitiesEngine()
        {
            var context = new Context(typeof(SparsePool<>));
            Context = context;
            sr_contextBinding = context;
            sr_contextRuntime = context;
        }

        public GameObject entitiesBootstrap;
        
        void Awake()
        {
            CallStarter();
            sr_contextRuntime.Init();
            sr_contextRuntime.RunAwake();
        }

        private void CallStarter()
        {
            if (entitiesBootstrap == null) return;
            var bootstrap = entitiesBootstrap.GetComponent<IEntitiesBootstrap>();
            bootstrap.OnStartBootstrap(sr_contextBinding);
        }
        
        void Start()
        {
            sr_contextRuntime.RunStart();
        }

        void Update()
        {
            sr_contextRuntime.RunUpdate();
        }

        void FixedUpdate()
        {
            sr_contextRuntime.RunFixedUpdate();
        }

        void LateUpdate()
        {
            sr_contextRuntime.RunLateUpdate();
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("CRYSTAL_ENTITIES/Create/Entities Engine", true)]
        private static bool CanCreateEngine()
        {
            return (FindObjectOfType<EntitiesEngine>() == null);
        }

        [UnityEditor.MenuItem("CRYSTAL_ENTITIES/Create/Entities Engine")]
        private static void CreateEngine()
        {
            var main_obj = new GameObject("CRYSTAL_ENTITIES_EntitiesEngine", typeof(EntitiesEngine)) { isStatic = true };
            UnityEditor.Selection.activeObject = main_obj;
        }
#endif
    };
}
