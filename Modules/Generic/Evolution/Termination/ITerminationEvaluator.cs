namespace Chinchillada.Generation.Evolution
{
    public interface ITerminationEvaluator
    {
        bool Evaluate(IEvolution evolutionController);
    }
}