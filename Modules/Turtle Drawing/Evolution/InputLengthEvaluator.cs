namespace Chinchillada.Generation.Turtle
{
    using System;
    using Chinchillada.Generation.Evolution;

    [Serializable]
    public class InputLengthEvaluator : IMetricEvaluator<string>
    {
        public float Evaluate(string item)
        {
            return item.Length;
        }
    }
}