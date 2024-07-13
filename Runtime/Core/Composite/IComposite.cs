namespace CrystalEntities
{
    public interface IComposite
    {
        void BindSystems(IContextBinding context);
        void BindRequests(IContextBinding context);
        void BindEvents(IContextBinding context);
    };
}
