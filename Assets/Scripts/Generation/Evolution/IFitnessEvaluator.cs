namespace Chinchillada.Generation.Evolution
{
    public interface IFitnessEvaluator<T>
    {
        float EvaluateFitness(T item);
    }
}