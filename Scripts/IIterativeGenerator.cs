using System;
using System.Collections;
using System.Collections.Generic;

namespace Chinchillada.Generation
{
    public interface IIterativeGenerator<T> : IGenerator<T>
    {
        IEnumerable<T> GenerateAsync();
        IEnumerator GenerateAsyncRoutine(Action<T> callback = null);
    }


}