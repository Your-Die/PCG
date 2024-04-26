using System.Collections.Generic;

namespace Chinchillada.PCG.Evolution
{
    public interface IOffspringGenerator<T>
    {
        IEnumerable<T> GenerateOffspring(IEnumerable<T> candidates, int amount, IRNG random);
    }
}