using System;
using System.Collections;
using System.Collections.Generic;

namespace Chinchillada.Generation
{
    public interface IAsyncGenerator<T> : IGenerator<T>
    {
        IEnumerable<T> GenerateAsync();
        IEnumerator GenerateAsyncRoutine(Action<T> callback = null);
    }

    public static class GeneratorExtensions
    {
        public static IEnumerable<T> Generate<T>(this IAsyncGenerator<T> generator, int amount)
        {
            for (var i = 0; i < amount; i++)
                yield return generator.Generate();
        }
    }
}