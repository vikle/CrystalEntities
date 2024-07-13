using UnityEngine;
using Object = UnityEngine.Object;

namespace CrystalEntities
{
    [DisallowMultipleComponent]
    public abstract class UnityObjectBaker<T> : MonoBehaviour, IEntityActorCallbackReceiver where T : Object
    {
        public T reference;

        void Reset()
        {
            reference = GetComponent<T>();
        }

        public virtual void OnAfterInitEntity(EntityView entity)
        {
            entity.Add(reference);
        }

        public virtual void OnBeforeDisposeEntity(EntityView entity) { }
    };
}
