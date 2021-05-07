namespace Chinchillada.Turtle
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Distributions;
    using Chinchillada;
    using Generation;
    using System.Linq;
    using Sirenix.Serialization;
    using UnityEngine;

    [Serializable]
    public class LSystemRuleGenerator : GeneratorBase<Dictionary<char, string>>
    {
        [SerializeField] private char[] requiredSymbols;

        [OdinSerialize] private IDiscreteDistribution<char> allowedCharacters;

        [OdinSerialize] private IDiscreteDistribution<int> replacementLength;

        [OdinSerialize] private IDiscreteDistribution<int> ruleCount;

        [SerializeField] private bool logRules;
        
        protected override Dictionary<char, string> GenerateInternal()
        {
            var dictionary = new Dictionary<char, string>();

            var symbols = this.ChooseSymbols();

            foreach (var symbol in symbols)
            {
                var replacement = this.GenerateReplacement();
                dictionary[symbol] = replacement;
            }

            if (this.logRules)
            {
                this.Print(dictionary);
            }
            
            return dictionary;
        }

        private void Print(Dictionary<char, string> dictionary)
        {
            var items = dictionary.Select(pair => $"{pair.Key} -> {pair.Value}");
            var text  = string.Join(", ", items);
            
            Debug.Log(text);
        }

        private string GenerateReplacement()
        {
            var length  = this.replacementLength.Sample(this.Random);
            var builder = new StringBuilder();

            for (var i = 0; i < length; i++)
            {
                var character = this.allowedCharacters.Sample(this.Random);
                builder.Append(character);
            }

            return builder.ToString();
        }

        private IEnumerable<char> ChooseSymbols()
        {
            var count = this.ruleCount.Sample(this.Random);

            if (count <= this.requiredSymbols.Length)
                return this.requiredSymbols;

            var missingCount = count - this.requiredSymbols.Length;

            var validSymbols = this.allowedCharacters.Support().Except(this.requiredSymbols);
            var extraSymbols =    validSymbols.ChooseRandomDistinct(missingCount, this.Random);

            return this.requiredSymbols.Concat(extraSymbols);
        }
    }
}