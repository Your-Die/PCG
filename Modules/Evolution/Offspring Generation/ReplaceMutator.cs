using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    public class ReplaceMutator<T> : Mutator<T>
    {
        [SerializeField] private IAsyncGenerator<T> generator;
        
        public override T Mutate(T _, IRNG random) => this.generator.Generate();
    }
}