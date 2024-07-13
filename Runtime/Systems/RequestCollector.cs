namespace CrystalEntities
{
    public sealed class RequestCollector<T> 
        : Handler<T>
        , IUpdateSystem where T : struct, IRequest
    {
        protected override void OnHandle(int entity)
        {
            if (m_data1[entity].IsCompleted)
            {
                m_data1.Remove(entity);
            }
        }
    };
}
