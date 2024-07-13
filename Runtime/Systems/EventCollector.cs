namespace CrystalEntities
{
    public sealed class EventCollector<T> 
        : Handler<T>
        , IUpdateSystem where T : struct, IEvent
    {
        protected override void OnHandle(int entity)
            => m_data1.Remove(entity);
    };
}
