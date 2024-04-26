using System.Collections.Generic;

namespace Chinchillada.PCG.Evolution
{
    public interface ISurvivorSelector
    {
        IEnumerable<IGenotype> SelectSurvivors(IEnumerable<IGenotype> parents, IEnumerable<IGenotype> children);
    }
}