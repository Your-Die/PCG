using Random = Chinchillada.Utilities.Random;

namespace Chinchillada.Generation.Evolution.Chromosome
{
    public class GeneGenerator : GeneMutator
    {
        protected override float MutateGene(float _) => Random.Float();
    }
}