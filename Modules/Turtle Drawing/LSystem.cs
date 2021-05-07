namespace Chinchillada.Generation.Turtle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Serializable]
    public class LSystem
    {
        private Dictionary<char, string> rules;

        public LSystem(char symbol, string replacement)
        {
            this.rules = new Dictionary<char, string>
            {
                {symbol, replacement}
            };
        }

        public LSystem(Dictionary<char, string> rules)
        {
            this.rules = rules;
        }

        public IEnumerable<char> Rewrite(IEnumerable<char> text)
        {
            foreach (var character in text)
            {
                if (this.rules.TryGetValue(character, out var replacement))
                {
                    foreach (var replacementCharacter in replacement)
                        yield return replacementCharacter;
                }
                else
                {
                    yield return character;
                }
            }
        }

        public IEnumerable<char> Rewrite(IEnumerable<char> text, int iterationCount)
        {
            for (var i = 0; i < iterationCount; i++)
                text = this.Rewrite(text);

            return text;
        }

        public override string ToString()
        {
            var ruleTexts = this.rules.Select(rule => $"{rule.Key} -> {rule.Value}");
            return string.Join(Environment.NewLine, ruleTexts);
        }
    }
}