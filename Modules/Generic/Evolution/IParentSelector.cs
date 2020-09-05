using System.Collections.Generic;

namespace Chinchillada.Generation.Evolution
{
    public interface IParentSelector
    {
        IEnumerable<IGenotype> SelectParents(IEnumerable<IGenotype> population);
    }
}