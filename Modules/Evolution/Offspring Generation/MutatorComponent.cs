namespace Chinchillada.PCG.Evolution
{
    using Sirenix.Serialization;

    public class MutatorComponent<T> : AutoRefBehaviour, IMutator<T>
    {
        [OdinSerialize] private IMutator<T> mutator;

        public T Mutate(T parent, IRNG random)
        {
            return this.mutator.Mutate(parent, random);
        }
    }
}