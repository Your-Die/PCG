using System;

namespace Chinchillada.PCG.Evolution
{
    public interface IEvolution
    {
        IGenotype FittestIndividual { get; }

        event Action EvolutionStarted;
    }
}