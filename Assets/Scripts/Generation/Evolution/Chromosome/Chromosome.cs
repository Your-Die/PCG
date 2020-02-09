using System.Collections;
using System.Collections.Generic;
using Utilities.Common;

namespace Chinchillada.Generation.Evolution.Chromosome
{
    public class Chromosome : IIndexable<float>
    {
        private float[] genes;

        public float this[int index]
        {
            get => this.genes[index];
            set => this.genes[index] = value;
        }

        public int Length => this.genes.Length;
        
        public Chromosome(int length)
        {
            this.genes = new float[length];
        }

        private Chromosome(Chromosome chromosome)
        {
            this.genes = new float[chromosome.Length];
            for (var i = 0; i < chromosome.Length; i++) 
                this.genes[i] = chromosome[i];
        }

        public IEnumerator<float> GetEnumerator()
        {
            return ((IEnumerable<float>) this.genes).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Chromosome Copy() => new Chromosome(this);
    }
}