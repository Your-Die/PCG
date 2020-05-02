namespace Chinchillada.Generation.Evolution
{
    public interface IMutator<T>
    {
        float Chance { get; }
        T Mutate(T parent);
    }
}