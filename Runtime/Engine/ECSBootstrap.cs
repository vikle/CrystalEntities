using UnityEngine;

namespace CrystalEntities
{
    public abstract class ECSBootstrap : MonoBehaviour
    {
        public abstract void OnStartBootstrap(IContextBinding context);
    };
}
