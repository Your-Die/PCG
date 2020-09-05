using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.Grammar
{
    [Serializable]
    public class ReferenceRule : IGrammarRuleDefinition
    {
        [SerializeField]
        private string symbol = GrammarConstants.OriginSymbol;

        [SerializeField] private IGrammarDefinition definition;

        public string Symbol => this.symbol;
        public List<string> Replacements => this.definition.FindRule(this.symbol);
    }
}