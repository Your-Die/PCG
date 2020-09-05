namespace Chinchillada.Generation.Evolution
{
    public interface ICrossover<T>
    {
        T Crossover(T parent1, T parent2);
    }
}