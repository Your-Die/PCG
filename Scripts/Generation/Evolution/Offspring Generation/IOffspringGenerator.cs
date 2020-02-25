using System.Collections.Generic;

namespace Chinchillada.Generation.Evolution
{
    public interface IOffspringGenerator<T>
    {
        IEnumerable<T> GenerateOffspring(IEnumerable<T> candidates, int amount);
    }
}