namespace Chinchillada.Generation.Evolution
{
    public interface IMetricEvaluator<T>
    {
        float Evaluate(T item);
    }
}