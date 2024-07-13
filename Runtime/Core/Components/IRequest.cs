namespace CrystalEntities
{
    public interface IRequest : IFragment
    {
        bool IsCompleted { get; set; }
    };
}
