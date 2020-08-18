namespace Mutiny.Grammar.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    public class MockGrammarDefinition : IGrammarDefinition
    {
        public string Name { get; set; }
        public IEnumerable<string> Symbols { get; set; }
        public string Origin { get; set; }
        public GrammarRuleDefinition OriginRule { get; set; }
        public List<GrammarRuleDefinition> Rules { get; set; }

        public MockGrammarDefinition(GrammarRuleDefinition originRule, params GrammarRuleDefinition[] rules)
        {
            this.OriginRule = originRule;
            this.Rules = rules.ToList();

            this.Origin = originRule.Symbol;
            this.Symbols = this.GetAllRules().Select(rule => rule.Symbol);
        }

        public MockGrammarDefinition(){}
    }
}