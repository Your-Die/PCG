using System.Linq;
using Chinchillada.Utilities;

namespace Chinchillada.Generation.Grammar
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Grammar/Grammar")]
    public class GrammarRuleSet : ScriptableObject
    {
        [SerializeField] private List<Rule> rules;

        public IEnumerable<Rule> Rules => this.rules;

        public BucketSet<Symbol, Expansion> ToExpansionDictionary()
        {
            var bucketSet = new BucketSet<Symbol, Expansion>();

            foreach (var rule in this.rules)
            {
                var symbol = rule.Symbol;
                
                foreach (var expansion in rule.Expansions) 
                    bucketSet.Add(symbol, expansion);
            }

            return bucketSet;
        }

        public IEnumerable<Symbol> GetAllSymbols() => this.rules.SelectMany(rule => rule.GetAllSymbols()).Distinct();
    }
}