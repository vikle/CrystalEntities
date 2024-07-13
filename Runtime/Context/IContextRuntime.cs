using System;

namespace CrystalEntities
{
    public interface IContextRuntime
    {
        void Init();
        void RunAwake();
        void RunStart();
        void RunFixedUpdate();
        void RunUpdate();
        void RunLateUpdate();
    };
}
