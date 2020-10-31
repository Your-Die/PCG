using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation.Evolution
{
    public class MutatorComponent<T> : ChinchilladaBehaviour, IMutator<T>
    {
        [SerializeField] private IMutator<T> mutator;
        public float Chance => this.mutator.Chance;

        public T Mutate(T parent)
        {
            return this.mutator.Mutate(parent);
        }
    }
}