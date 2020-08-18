using NUnit.Framework;

namespace Mutiny.Grammar.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [TestOf(typeof(GrammarParser))]
    public class GrammarParserTests
    {
        private Dictionary<string, NonTerminal> tokenDictionary;

        [SetUp]
        public void Init()
        {
            var originToken = CreateOriginToken();
            this.tokenDictionary = new Dictionary<string, NonTerminal>
            {
                {originToken.Key, originToken}
            };
        }


        // A Test behaves as an ordinary method
        [Test]
        public void CreateFlatRule()
        {
            var ruleDefinition = CreateAnimalRule(Constants.OriginSymbol);

            var rule = GrammarParser.ConstructRule(ruleDefinition, this.tokenDictionary);

            Assert.That(rule.Replacements, Has.Count.EqualTo(4));
            Assert.That(rule.Replacements, Is.Unique);
        }

        [Test]
        public void NoOrigin()
        {
            var animal = "animal";
            var mockGrammarDefinition = new MockGrammarDefinition
            {
                Rules = new List<GrammarRuleDefinition>
                {
                    CreateAnimalRule(animal)
                },
                Symbols = new[] {animal}
            };

            Assert.Throws<ArgumentException>(() => GrammarParser.Construct(mockGrammarDefinition));
        }


        [Test]
        public void InjectsNonTerminalReference()
        {
            var (grammar, animalRuleDefinition) = CreateNestedFlatGrammar();

            var originRule = GetRuleAsserted(grammar, Constants.OriginSymbol);
            var animalRule = GetRuleAsserted(grammar, animalRuleDefinition.Symbol);

            Assert.That(originRule.Replacements, Has.Count.EqualTo(1));
            var replacement = originRule.Replacements.First();

            Assert.That(replacement.Tokens, Contains.Item(animalRule.Symbol));
        }

        [Test]
        public void TerminalHead()
        {
            const string animalSymbol = "animal";
            var replacement = $"#{animalSymbol}#";
            var originRuleDefinition = new GrammarRuleDefinition(Constants.OriginSymbol, replacement);
            var animalRuleDefinition = new GrammarRuleDefinition(animalSymbol, "dog");

            var grammarDefinition = new MockGrammarDefinition(originRuleDefinition, animalRuleDefinition);
            var grammar = GrammarParser.Construct(grammarDefinition);

            // 2 rules.
            Assert.That(grammar.Rules.Count(), Is.EqualTo(2));

            // One replacement for origin.
            Assert.That(grammar.OriginRule.Replacements.Count, Is.EqualTo(1));

            // That replacement is has 1 token.
            Assert.That(grammar.OriginRule.Replacements[0].Count(), Is.EqualTo(1));
        }


        [Test]
        public void NestedFlat()
        {
            var animalSymbolText = "animal";
            var (grammar, animalRuleDefinition) = CreateNestedFlatGrammar(animalSymbolText);

            var animalRule = GetRuleAsserted(grammar, animalSymbolText);

            // a replacement for each replacement definition.
            Assert.That(animalRule.Replacements, Has.Count.EqualTo(animalRuleDefinition.Replacements.Count));

            // Confirm that each replacement consists of only 1 token.
            foreach (var replacement in animalRule.Replacements)
                Assert.That(replacement.Tokens, Has.Count.EqualTo(1));

            // Assert that all definitions have a corresponding token.
            var tokens = animalRule.Replacements.Select(replacement => replacement.Tokens.First().Key).ToList();
            foreach (var replacementDefinition in animalRuleDefinition.Replacements)
                Assert.That(tokens, Contains.Item(replacementDefinition));
        }

        [Test]
        public static void SimpleCondition()
        {
            var animalSymbolText = "animal";
            var originRuleDefinition = new GrammarRuleDefinition(Constants.OriginSymbol, $"#{animalSymbolText}#");
            var animalRule = new GrammarRuleDefinition(animalSymbolText, "[animal: =dog] dog");

            var grammarDefinition = new MockGrammarDefinition(originRuleDefinition, animalRule);
            var grammar = GrammarParser.Construct(grammarDefinition);

            Assert.That(grammar.Rules.Count(), Is.EqualTo(2));
            Assert.That(grammar.OriginRule.Replacements.Count, Is.EqualTo(1));
        }

        [Test]
        public static void SimpleEffect()
        {
            var animalSymbolText = "animal";
            var originRuleDefinition = new GrammarRuleDefinition(Constants.OriginSymbol, $"#{animalSymbolText}#");
            var animalRule = new GrammarRuleDefinition(animalSymbolText, "dog [animal: dog]");

            var grammarDefinition = new MockGrammarDefinition(originRuleDefinition, animalRule);
            var grammar = GrammarParser.Construct(grammarDefinition);

            Assert.That(grammar.Rules.Count(), Is.EqualTo(2));
            Assert.That(grammar.OriginRule.Replacements.Count, Is.EqualTo(1));
        }

        [Test]
        public static void WhiteSpaceBeforeCondition()
        {
            var animalSymbolText = "animal";
            var originRuleDefinition = new GrammarRuleDefinition(Constants.OriginSymbol, $"#{animalSymbolText}#");
            var animalRule = new GrammarRuleDefinition(animalSymbolText, "  [animal: =dog] dog");

            var grammarDefinition = new MockGrammarDefinition(originRuleDefinition, animalRule);
            var grammar = GrammarParser.Construct(grammarDefinition);

            Assert.That(grammar.Rules.Count(), Is.EqualTo(2));
            Assert.That(grammar.OriginRule.Replacements.Count, Is.EqualTo(1));
        }

        [Test]
        public static void WhiteSpaceAfterEffect()
        {
            var animalSymbolText = "animal";
            var originRuleDefinition = new GrammarRuleDefinition(Constants.OriginSymbol, $"#{animalSymbolText}#");
            var animalRule = new GrammarRuleDefinition(animalSymbolText, "dog [animal: dog]   ");

            var grammarDefinition = new MockGrammarDefinition(originRuleDefinition, animalRule);
            var grammar = GrammarParser.Construct(grammarDefinition);

            Assert.That(grammar.Rules.Count(), Is.EqualTo(2));
            Assert.That(grammar.OriginRule.Replacements.Count, Is.EqualTo(1));
        }


        #region Helper functions

        private static NonTerminal CreateOriginToken() => new NonTerminal(Constants.OriginSymbol);


        private static GrammarRule GetRuleAsserted(Grammar grammar, string symbol)
        {
            Assert.True(grammar.Rules.Count(IsRule(symbol)) == 1);
            return grammar.Rules.First(IsRule(symbol));

            Func<GrammarRule, bool> IsRule(string symbolText) => (rule) => rule.Symbol.Key == symbolText;
        }

        private static (Grammar grammar, GrammarRuleDefinition animalRuleDefinition) CreateNestedFlatGrammar(
            string nestedSymbol = "animal")
        {
            var sentence = $"i like #{nestedSymbol}#";

            var nestedRule = new GrammarRuleDefinition(Constants.OriginSymbol, sentence);
            var animalRuleDefinition = CreateAnimalRule(nestedSymbol);

            var grammarDefinition = new MockGrammarDefinition(nestedRule, animalRuleDefinition);

            var grammar = GrammarParser.Construct(grammarDefinition);

            return (grammar, animalRuleDefinition);
        }

        private static GrammarRuleDefinition CreateAnimalRule(string symbol = "animal")
        {
            string[] words =
            {
                "dog",
                "cat",
                "caique",
                "macaw"
            };

            var ruleDefinition = new GrammarRuleDefinition(symbol, words);
            return ruleDefinition;
        }

        #endregion
    }
}