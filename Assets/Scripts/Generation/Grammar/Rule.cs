using System.Linq;

namespace Chinchillada.Generation.Grammar
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Rule
    {
        [SerializeField] private Symbol leftHandSide;

        [SerializeField] private List<Expansion> rightHandSide;

        public Symbol Symbol => leftHandSide;

        public IEnumerable<Expansion> Expansions => rightHandSide;

        public IEnumerable<Symbol> GetAllSymbols()
        {
            var expansionSymbols = this.rightHandSide.SelectMany(expansion => expansion.Symbols);
            var symbols = expansionSymbols.Prepend(this.leftHandSide);

            return symbols.Distinct();
        }

        public override string ToString()
        {
            var expansions = string.Join(" | ", this.rightHandSide);
            return $"{this.leftHandSide} -> {expansions}";
        }
    }
}