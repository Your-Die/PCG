using System;
using System.Collections.Generic;

namespace Chinchillada.Generation
{
    public interface IGenerator<out T>
    {
        T Generate();
    }

    public interface IObservableGenerator<out T> : IGenerator<T>
    {
        event Action<T> Generated;
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