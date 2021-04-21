using System.Collections.Generic;

namespace Chinchillada.Generation.Evolution
{
    public class Genotype<T> : IGenotype
    {
        public T Candidate { get; }
        public float Fitness { get; }

        public Genotype(T candidate, float fitness)
        {
            this.Candidate = candidate;
            this.Fitness = fitness;
        }

        public override string ToString() => $"[{this.Fitness}] {this.Candidate}";
    }

    public interface IGenotype
    {
        float Fitness { get; }
    }

    public class GenotypeComparer : IComparer<IGenotype>
    {
        public int Compare(IGenotype x, IGenotype y)
        {
            return y.Fitness.CompareTo(x.Fitness);
        }
    }
}