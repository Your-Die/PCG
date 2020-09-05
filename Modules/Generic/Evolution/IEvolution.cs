using System;

namespace Chinchillada.Generation.Evolution
{
    public interface IEvolution
    {
        IGenotype FittestIndividual { get; }

        event Action EvolutionStarted;
    }
}