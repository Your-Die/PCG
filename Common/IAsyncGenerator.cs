using System;
using System.Collections;
using System.Collections.Generic;

namespace Chinchillada.Generation
{
    using Behavior;

    public interface IAsyncGenerator<T> : IGenerator<T>, IExecutable
    {
        IEnumerable<T> GenerateAsync();
        IEnumerator GenerateAsyncRoutine(Action<T> callback = null);
    }


}