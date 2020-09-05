﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Chinchillada.Foundation;

namespace Mutiny.Grammar
{
    public interface IGrammarDefinition
    {
        string Name { get; }
        string Origin { get; set; }
        List<GrammarRuleDefinition> Rules { get; set; }
    }

    
    public static class GrammarDefinitionExtensions
    {
        public static IEnumerable<string> GetSymbols(this IGrammarDefinition grammarDefinition)
        {
            yield return Constants.OriginSymbol;

            foreach (var rule in grammarDefinition.Rules)
                yield return rule.Symbol;
        }

        public static GrammarRuleDefinition BuildOriginRule(this IGrammarDefinition grammarDefinition)
        {
            return new GrammarRuleDefinition(Constants.OriginSymbol, grammarDefinition.Origin);
        }

        public static IEnumerable<GrammarRuleDefinition> GetAllRules(this IGrammarDefinition definition)
        {
            return definition.Rules.Prepend(definition.BuildOriginRule());
        }
    }
}