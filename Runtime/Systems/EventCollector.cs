namespace CrystalEntities
{
    public sealed class EventCollector<T> 
        : Processor<T>
        , IUpdateSystem where T : struct, IEvent
    {
        protected override void OnExecute(IContext context, int entity)
        {
            m_data1.Remove(entity);
        }
    };
}
