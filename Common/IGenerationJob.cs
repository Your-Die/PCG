namespace Chinchillada.Generation
{
    using System;

    public interface IGenerationJob<out T>
    {
        bool IsFinished { get; }

        T Result { get; }

        event Action<T> Finished;
    }

    public interface IGenerationJobExecutor<in TJob, out TResult> where TJob : IGenerationJob<TResult>
    {
        TResult Generate(TJob job);
    }

    public class GenerationJobBase<T> : IGenerationJob<T>
    {
        public bool IsFinished { get; private set; }
        public T    Result     { get; private set; }

        public event Action<T> Finished;

        public void RegisterResult(T result)
        {
            this.Result     = result;
            this.IsFinished = true;

            this.Finished?.Invoke(result);
        }
    }
}