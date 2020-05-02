using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    public class ReplaceMutator<T> : Mutator<T>
    {
        [SerializeField] private IGenerator<T> generator;
        
        public override T Mutate(T _) => this.generator.Generate();
    }
}