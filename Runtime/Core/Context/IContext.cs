using System;
using System.Collections.Generic;

namespace CrystalEntities
{
    public interface IContext
    {
        IReadOnlyList<bool> Entities { get; }
        int EntitiesCount { get; }

        int CreateEntity();
        void DestroyEntity(int entity);

        bool IsAlive(int entity);

        ref T Trigger<T>(int entity) where T : struct, IEvent;

        ref T Begin<T>(int entity) where T : struct, IRequest;
        void End<T>(int entity) where T : struct, IRequest;

        bool HasFragment<T>(int entity) where T : struct, IFragment;
        ref T AddFragment<T>(int entity) where T : struct, IFragment;
        ref T GetFragment<T>(int entity) where T : struct, IFragment;
        void DisposeFragment<T>(int entity) where T : struct, IFragment, IDisposable;
        void RemoveFragment<T>(int entity) where T : struct, IFragment;

        IPool<T> GetPool<T>() where T : struct, IFragment;

        ContextEnumerator GetEnumerator();
    };
}
