using System.Collections.Generic;

#if ENABLE_IL2CPP
    using Unity.IL2CPP.CompilerServices;
#endif

namespace CrystalEntities
{
#if ENABLE_IL2CPP
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
#endif
    public static class ArrayEx
    {
        const int k_DefaultCapacity = 8;
        
        public static void EnsureCapacity<T>(ref T[] array, int min)
        {
            int current_capacity = array.Length;
            
            if (TryComputeCapacity(current_capacity, min, out int new_capacity) == false)
            {
                return;
            }
            
            var new_array = new T[new_capacity];

            for (int i = 0; i < current_capacity; i++)
            {
                new_array[i] = array[i];
            }

            array = new_array;
        }

        public static bool TryComputeCapacity(int current, int min, out int newCapacity)
        {
            if (current < min)
            {
                newCapacity = (current == 0) ? k_DefaultCapacity : (current << 1);
                if (newCapacity < min) newCapacity = min;
                return true;
            }
            
            newCapacity = -1;
            return false;
        }
        
        public static void WhereCast<T, TV>(IReadOnlyList<T> source, out TV[] result) 
            where T : class 
            where TV : class
        {
            WhereCast(source, out List<TV> list);
            result = list.ToArray();
        }
        
        public static void WhereCast<T, TV>(IReadOnlyList<T> source, out List<TV> result)
            where T : class 
            where TV : class
        {
            result = new List<TV>(source.Count);

            for (int i = 0, i_max = source.Count; i < i_max; i++)
            {
                if (source[i] is TV value)
                {
                    result.Add(value);
                }
            }
        }
    };
}
