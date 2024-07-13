using System;

namespace CrystalEntities
{
    public interface IContextRuntime
    {
        void Init();
        void RunStart();
        void RunFixedUpdate();
        void RunUpdate();
        void RunLateUpdate();
    };
}
