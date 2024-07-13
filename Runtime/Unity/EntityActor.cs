using System.Runtime.CompilerServices;
using UnityEngine;

namespace CrystalEntities
{
#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
    [Il2CppEagerStaticClassConstruction]
#endif
    [DisallowMultipleComponent]
    public abstract class EntityActor : MonoBehaviour
    {
        static readonly IContext sr_context;
        IEntityActorCallbackReceiver[] m_receivers;

        public EntityView Entity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]private set;
        }

        static EntityActor()
        {
            sr_context = EntitiesEngine.Context;
        }

        protected EntityActor()
        {
            Entity = EntityView.Empty;
        }

        protected virtual void Awake()
        {
            m_receivers = GetComponents<IEntityActorCallbackReceiver>();
        }

        protected virtual void OnEnable()
        {
            InitEntity();
        }

        protected virtual void OnDisable()
        {
            DisposeEntity();
        }

        public void InitEntity()
        {
            if (Entity.IsValid) return;
            Entity = EntityView.CreateEntity(sr_context);
            OnAfterInitEntity();
            CallReceivers();
        }

        protected virtual void OnAfterInitEntity()
        {
            Entity.Add(gameObject);
            Entity.Add(transform);
            Entity.Add(this);
        }

        private void CallReceivers()
        {
            for (int i = 0, i_max = m_receivers.Length; i < i_max; i++)
            {
                m_receivers[i].OnAfterInitEntity(Entity);
            }
        }

        public void DisposeEntity()
        {
            if (Entity.IsValid == false) return;
            OnBeforeDisposeEntity();
            Entity.DestroyEntity();
            Entity = EntityView.Empty;
        }

        protected virtual void OnBeforeDisposeEntity()
        {
            for (int i = 0, i_max = m_receivers.Length; i < i_max; i++)
            {
                m_receivers[i].OnBeforeDisposeEntity(Entity);
            }
        }
    };
}
