using System.Collections.Generic;

namespace Chinchillada.Generation.Evolution
{
    public interface ISurvivorSelector
    {
        IEnumerable<IGenotype> SelectSurvivors(IEnumerable<IGenotype> parents, IEnumerable<IGenotype> children);
    }
}