using System;

namespace Chinchillada.Generation
{
    public interface IGenerator<T>
    {
        T Result { get; }
        event Action<T> Generated;
        T Generate();
    }
}