namespace Chinchillada.Generation.Evolution
{
    public interface IMutator<T>
    {
        T Mutate(T parent, IRNG random);
    }
}