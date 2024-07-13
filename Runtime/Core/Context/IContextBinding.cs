using System;

namespace CrystalEntities
{
    public interface IContextBinding
    {
        IContextBinding BindComposite<T>() where T : class, IComposite, new();
        IContextBinding BindRequest<T>() where T : struct, IRequest;
        IContextBinding BindEvent<T>() where T : struct, IEvent;
        IContextBinding BindSystem<T>() where T : class, ISystem, new();
        IContextBinding Inject(object data);
    };
}
