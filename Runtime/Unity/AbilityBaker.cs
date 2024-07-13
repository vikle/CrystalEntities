using UnityEngine;

namespace CrystalEntities
{
    [RequireComponent(typeof(EntityActor))]
    public abstract class AbilityBaker : MonoBehaviour, IEntityActorCallbackReceiver
    {
        public abstract void OnAfterInitEntity(EntityView entity);
        public virtual void OnBeforeDisposeEntity(EntityView entity) { }
    };
}

