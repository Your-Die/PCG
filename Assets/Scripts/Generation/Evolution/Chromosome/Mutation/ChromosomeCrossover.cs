using System;
using Random = Chinchillada.Utilities.Random;

namespace Chinchillada.Generation.Evolution.Chromosome
{
    [Serializable]
    public class ChromosomeCrossover : ICrossover<Chromosome>
    {
        public Chromosome Crossover(Chromosome parent1, Chromosome parent2)
        {
            var (smaller, bigger) = parent1.Length < parent2.Length
                ? (parent1, parent2)
                : (parent2, parent1);

            var child = bigger.Copy();
            var crossoverPoint = Random.Range(smaller.Length);

            for (var i = 0; i <= crossoverPoint; i++) 
                child[i] = smaller[i];

            return child;
        }
    }
}