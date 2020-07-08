using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using UnityEngine;

namespace Chinchillada.Generation.Grammar
{
    public class GrammarGenerator : GeneratorBase<IEnumerable<Symbol>>
    {
        [SerializeField] private List<Symbol> startingSymbols;
        
        [SerializeField] private Grammar grammar;

        [SerializeField] private int iterations = 1;

        private bool isFinished;
        
        protected override IEnumerable<Symbol> GenerateInternal()
        {
            return this.GenerateAsync().Last();
        }

        public override IEnumerable<IEnumerable<Symbol>> GenerateAsync()
        {
            var ruleDictionary = this.grammar.Rules.ToDictionary(
                rule => rule.LHS,
                rule => rule);

            var symbolSequence = this.startingSymbols;
            yield return symbolSequence;
            
            for (var i = 0; i < this.iterations; i++)
            {
                symbolSequence = this.Iterate(symbolSequence, ruleDictionary).ToList();
                yield return symbolSequence;

                if (this.isFinished)
                    break;
            }
        }

        private IEnumerable<Symbol> Iterate(IEnumerable<Symbol> symbols, IReadOnlyDictionary<Symbol, Rule> ruleLookup)
        {
            var changesMade = false;
            
            foreach (var symbol in symbols)
            {
                if (ruleLookup.TryGetValue(symbol, out var rule))
                {
                    changesMade = true;
                    var expansion = rule.RHS.ChooseRandom();
                    
                    foreach (var newSymbol in expansion.Symbols)
                        yield return newSymbol;
                }
                else
                    yield return symbol;
            }

            if (!changesMade) 
                this.isFinished = true;
        }
    }
}