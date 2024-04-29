using System;
using System.Collections;
using System.Collections.Generic;

namespace Chinchillada.PCG
{
    public interface IAsyncGenerator<T> : IGenerator<T>
    {
        T Result { get; }

        event Action<T> Generated;
        
        IEnumerable<T> GenerateAsync(IRNG random);
        IEnumerator GenerateAsyncRoutine(IRNG random, Action<T> callback = null);
    }


}