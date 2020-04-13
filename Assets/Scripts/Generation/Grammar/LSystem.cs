using System.Collections.Generic;
using System.Linq;
using Chinchillada.Utilities;
using DefaultNamespace;
using UnityEngine;

namespace Chinchillada.Generation.Grammar
{
    public class LSystem : GeneratorBase<IEnumerable<Symbol>>
    {
        [SerializeField] private GrammarRuleSet ruleSet;

        [SerializeField] private Symbol origin;

        [SerializeField] private int expansionCount = 1;

        private BucketSet<Symbol, Expansion> expansionsBySymbol;

        public GrammarRuleSet RuleSet => this.ruleSet;

        public IEnumerable<Symbol> Expand(IEnumerable<Symbol> symbols) => symbols.SelectMany(this.Expand);

        public IEnumerable<Symbol> Expand(Symbol symbol)
        {
            if (this.expansionsBySymbol.ContainsKey(symbol))
            {
                var expansions = this.expansionsBySymbol[symbol];
                var expansion = expansions.ChooseRandom();

                foreach (var replacement in expansion.Symbols)
                    yield return replacement;
            }
            else yield return symbol;
        }

        protected override IEnumerable<IEnumerable<Symbol>> GenerateAsyncInternal()
        {
            yield return new[] {this.origin};

            for (var expansion = 0; expansion < this.expansionCount; expansion++)
                yield return this.Expand(this.Result);
        }

        protected override IEnumerable<Symbol> GenerateInternal()
        {
            var symbols = new[] {this.origin}.AsEnumerable();

            for (var expansion = 0; expansion < this.expansionCount; expansion++)
                symbols = this.Expand(symbols).ToList();

            return symbols;
        }

        protected override void Awake()
        {
            base.Awake();

            this.expansionsBySymbol = this.RuleSet.ToExpansionDictionary();
        }
    }
}