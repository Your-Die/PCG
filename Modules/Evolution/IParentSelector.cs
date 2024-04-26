using System.Collections.Generic;

namespace Chinchillada.PCG.Evolution
{
    public interface IParentSelector
    {
        IEnumerable<IGenotype> SelectParents(IEnumerable<IGenotype> population);
    }
}