using System.Collections.Generic;
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
        
        public static GrammarRuleDefinition FindRule(this IGrammarDefinition definition, string symbol)
        {
            return definition.Rules.Find(Match);

            bool Match(GrammarRuleDefinition match) => string.Equals(match.Symbol, symbol);
        }
        
        public static IEnumerable<GrammarRuleDefinition> GetAllRules(this IGrammarDefinition definition)
        {
            return definition.Rules.Prepend(definition.BuildOriginRule());
        }
        
        public static void ReplaceSymbol(this IGrammarDefinition grammar, string oldSymbol, string newSymbol)
        {
            if (grammar.Rules.TryFind(matchingRule => string.Equals(oldSymbol, matchingRule.Symbol), out var rule))
                grammar.ReplaceSymbol(rule, newSymbol);
        }

        public static void ReplaceSymbol(this IGrammarDefinition grammar, GrammarRuleDefinition rule, string symbol)
        {
            var oldSymbol = rule.Symbol;
            rule.Symbol = symbol;

            var regex = new Regex(Constants.SymbolRegex);
            var evaluator = new MatchEvaluator(ReplaceOldSymbol);

            foreach (var ruleDefinition in grammar.Rules)
            {
                for (var index = 0; index < ruleDefinition.Replacements.Count; index++)
                {
                    var replacement = ruleDefinition.Replacements[index];
                    var newReplacement = regex.Replace(replacement, evaluator);

                    if (string.Equals(replacement, newReplacement))
                        continue;

                    rule.Replacements[index] = newReplacement;
                }
            }

            var newOrigin = regex.Replace(grammar.Origin, evaluator);
            if (!string.Equals(grammar.Origin, newOrigin))
                grammar.Origin = newOrigin;

            string ReplaceOldSymbol(Match match)
            {
                var group = match.Groups[1];
                var capturedSymbol = group.Value;

                var newSymbol = string.Equals(capturedSymbol, oldSymbol) ? symbol : capturedSymbol;
                return $"{Constants.SymbolGuard}{newSymbol}{Constants.SymbolGuard}";
            }
        }
    }
}