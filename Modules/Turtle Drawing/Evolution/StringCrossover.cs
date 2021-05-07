namespace Chinchillada.Generation.Turtle
{
    using System;
    using System.Text;
    using Chinchillada;
    using Chinchillada.Generation.Evolution;

    [Serializable]
    public class StringCrossover : ICrossover<string>
    {
        public string Crossover(string parent1, string parent2, IRNG random)
        {
            var index1 = random.Range(parent1.Length);
            var index2 = random.Range(parent2.Length);

            var firstHalf  = parent1.Substring(0, index1);
            var secondHalf = parent2.Substring(index2);

            var builder = new StringBuilder();
            builder.Append(firstHalf);
            builder.Append(secondHalf);

            return builder.ToString();
        }
    }
}