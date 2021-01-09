namespace Chinchillada.Generation
{
    public interface ITerminationEvaluator
    {
        void Reset();
        
        bool Evaluate();
    }
    
    public interface ITerminationEvaluator<T>
    {
        void Reset();
        bool Evaluate(T context);
    }
}