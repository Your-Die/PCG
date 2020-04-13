using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chinchillada.Utilities;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Generation.Grammar
{
    public class GrammarTextIntepreter : GeneratorBase<string>
    {
        [SerializeField, FindComponent, Required]
        private LSystem lSystem;

        [SerializeField] private IDictionary<Symbol, string> symbolDictionary = new Dictionary<Symbol, string>();

        public string Interpret(IEnumerable<Symbol> symbols)
        {
            var stringBuilder = new StringBuilder();

            foreach (var symbol in symbols)
            {
                var text = this.symbolDictionary[symbol];
                stringBuilder.Append(text);
            }

            return stringBuilder.ToString();
        }

        protected override string GenerateInternal()
        {
            var symbols = this.lSystem.Generate();
            return this.Interpret(symbols);
        }

        protected override IEnumerable<string> GenerateAsyncInternal()
        {
            foreach (var symbolSet in this.lSystem.GenerateAsync())
                yield return this.Interpret(symbolSet);
        }

        [Button]
        private void PopulateDictionary()
        {
            if (this.symbolDictionary == null)
                this.symbolDictionary = new Dictionary<Symbol, string>();

            var symbols = this.lSystem.RuleSet.GetAllSymbols();
            var newSymbols = symbols.Except(this.symbolDictionary.Keys);

            foreach (var symbol in newSymbols)
                this.symbolDictionary.Add(symbol, string.Empty);
        }
    }
}