namespace Chinchillada.Generation.Turtle
{
    using System.Collections.Generic;
    using System.Linq;
    using Chinchillada;
    using Turtle;
    using Generators;
    using UnityEngine;

    public class LSystemGeneratorNode : GeneratorNodeWithPreview<LSystem>
    {
        [SerializeField] [Input] private char[] requiredSymbols;

        [SerializeField] [Input] private int    ruleCount;
      
        [SerializeField] [Input]  private string replacementInput;

        [SerializeField] private List<char> allowedSymbols;
       

        protected override LSystem GenerateInternal()
        {
            var symbols = this.GetSymbols();

            var rules = symbols.ToDictionary(
                symbol => symbol,
                _ => this.GenerateReplacement()
            );

            return new LSystem(rules);
        }

        private string GenerateReplacement()
        {
            return this.UpdateInput(ref this.replacementInput, nameof(this.replacementInput));
        }

        private IEnumerable<char> GetSymbols()
        {
            if (this.ruleCount <= this.requiredSymbols.Length)
                return this.requiredSymbols;

            var extraSymbols = ChooseSymbols();

            return this.requiredSymbols.Concat(extraSymbols);

            IEnumerable<char> ChooseSymbols()
            {
                var missingCount = this.ruleCount - this.requiredSymbols.Length;
                var validSymbols = this.allowedSymbols.Except(this.requiredSymbols);

                return validSymbols.ChooseRandomDistinct(missingCount, this.Random);
            }
        }

        protected override void UpdateInputs()
        {
            base.UpdateInputs();
            this.UpdateInput(ref this.ruleCount, nameof(this.ruleCount));
        }
    }
}