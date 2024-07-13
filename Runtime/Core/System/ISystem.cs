
namespace CrystalEntities
{
    public interface ISystem
    {
    };

    public interface IAwakeSystem : ISystem
    {
        void OnAwake();
    };
    
    public interface IStartSystem : ISystem
    {
        void OnStart();
    };

    public interface IUpdateSystem : ISystem
    {
        void OnUpdate();
    };
    
    public interface IFixedUpdateSystem : ISystem
    {
        void OnFixedUpdate();
    };

    public interface ILateUpdateSystem : ISystem
    {
        void OnLateUpdate();
    };
}
