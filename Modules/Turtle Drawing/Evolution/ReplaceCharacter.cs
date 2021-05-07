namespace Chinchillada.Generation.Turtle
{
    using System;
    using System.Text;
    using Chinchillada;
    using Chinchillada.Generation.Evolution;
    using Sirenix.Serialization;

    [Serializable]
    public class ReplaceCharacter : IMutator<string>
    {
        [OdinSerialize] private IGenerator<char> characterGenerator;

        public string Mutate(string parent, IRNG random)
        {
            var index     = random.Range(parent.Length);
            var character = this.characterGenerator.Generate();

            var builder = new StringBuilder(parent.Length)
            {
                [index] = character
            };
            
            return builder.ToString();
        }
    }
}