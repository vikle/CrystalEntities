
namespace CrystalEntities
{
    public interface ISystem
    {
    };
    
    public interface IStartSystem : ISystem
    {
        void OnStart(IContext context);
    };

    public interface IUpdateSystem : ISystem
    {
        void OnUpdate(IContext context);
    };
    
    public interface IFixedUpdateSystem : ISystem
    {
        void OnFixedUpdate(IContext context);
    };

    public interface ILateUpdateSystem : ISystem
    {
        void OnLateUpdate(IContext context);
    };
}
