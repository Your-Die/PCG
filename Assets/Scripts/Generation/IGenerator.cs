using System;
using System.Collections;
using System.Collections.Generic;

namespace Chinchillada.Generation
{
    public interface IGenerator<out T>
    {
        T Result { get; }
        
        event Action<T> Generated;
        
        T Generate();

        IEnumerable<T> GenerateAsync();
        IEnumerator GenerateAsyncRoutine(Action<T> callback = null);
    }

    public static class GeneratorExtensions
    {
        public static IEnumerable<T> Generate<T>(this IGenerator<T> generator, int amount)
        {
            for (var i = 0; i < amount; i++)
                yield return generator.Generate();
        }
    }
}