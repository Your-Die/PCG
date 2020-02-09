using UnityEngine;
using Random = Chinchillada.Utilities.Random;

namespace Chinchillada.Generation.Evolution.Chromosome
{
    public abstract class GeneMutator : Mutator<Chromosome>
    {
        [SerializeField] private float geneMutationChance = 0.2f;
        
        public override Chromosome Mutate(Chromosome parent)
        {
            var child = parent.Copy();
            
            for (var index = 0; index < child.Length; index++)
            {
                if (!Random.Bool(this.geneMutationChance))
                    continue;

                var gene = child[index];
                child[index] = this.MutateGene(gene);
            }

            return child;
        }

        protected abstract float MutateGene(float gene);

    }
}