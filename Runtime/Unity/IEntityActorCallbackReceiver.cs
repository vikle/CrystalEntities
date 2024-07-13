namespace CrystalEntities
{
    public interface IEntityActorCallbackReceiver
    {
        void OnAfterInitEntity(EntityView entity);
        void OnBeforeDisposeEntity(EntityView entity);
    };
}
