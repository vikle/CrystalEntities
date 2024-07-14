namespace CrystalEntities
{
    public sealed class RequestCollector<T> 
        : Processor<T>
        , IUpdateSystem where T : struct, IRequest
    {
        protected override void OnExecute(IContext context, int entity)
        {
            if (m_data1[entity].IsCompleted)
            {
                m_data1.Remove(entity);
            }
        }
    };
}
