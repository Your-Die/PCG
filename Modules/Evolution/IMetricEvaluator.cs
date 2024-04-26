namespace Chinchillada.PCG.Evolution
{
    public interface IMetricEvaluator<T>
    {
        float Evaluate(T item);
    }
}