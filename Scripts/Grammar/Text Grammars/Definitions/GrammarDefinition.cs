using System;
using System.Collections.Generic;
using System.Linq;
using Mutiny.Arcs;
using UnityEngine;

namespace Mutiny.Grammar
{
    using System.Text;
    using Sirenix.OdinInspector;
    using Sirenix.Utilities;

    [CreateAssetMenu(menuName = "Mutiny/Grammar Definition")]
    public class GrammarDefinition : SerializedScriptableObject, IGrammarDefinition
    {
        [SerializeField, MultiLineProperty(10), FoldoutGroup("Origin")]
        private string origin;

        [SerializeField] private List<GrammarRuleDefinition> rules = new List<GrammarRuleDefinition>();

        public string Name => this.name;

        public string Origin
        {
            get => this.origin;
            set => this.origin = value;
        }

        public List<GrammarRuleDefinition> Rules
        {
            get => this.rules;
            set => this.rules = value;
        }

        [Button]
        private void AddMissingSymbols()
        {
            var allRules = this.GetAllRules().ToList();
            var symbolSet = allRules.Select(rule => rule.Symbol).ToHashSet();

            var symbols = allRules.SelectMany(rule => rule.Replacements.SelectMany(ParseSymbols));

            foreach (var symbol in symbols)
            {
                if (symbolSet.Contains(symbol))
                    continue;

                this.Rules.Add(new GrammarRuleDefinition(symbol));
                symbolSet.Add(symbol);
            }
        }

        private static IEnumerable<string> ParseSymbols(string text)
        {
            var openSymbol = false;
            var symbolBuilder = new StringBuilder();

            foreach (var character in text)
            {
                if (openSymbol)
                {
                    if (character == Constants.SymbolGuard)
                    {
                        yield return symbolBuilder.ToString();

                        symbolBuilder.Clear();
                        openSymbol = false;
                    }
                    else
                    {
                        symbolBuilder.Append(character);
                    }
                }
                else
                {
                    if (character == Constants.SymbolGuard)
                        openSymbol = true;
                }
            }
        }

        [Button, FoldoutGroup("Actions")]
        private void CreateDebugOrigin()
        {
            var symbols = this.rules.Select(rule => WrapGuards(rule.Symbol));
            this.origin = string.Join(Environment.NewLine, symbols);

            string WrapGuards(string text) => $"{Constants.SymbolGuard}{text}{Constants.SymbolGuard}";
        }

        [ShowInInspector, ReadOnly, FoldoutGroup("Parameters", 2)]
        private List<GrammarParameter> parameters;

        [Button, FoldoutGroup("Parameters", 0)]
        private void FindParameters()
        {
            this.parameters = GrammarParameter.Extract(this).ToList();
        }

        [Button, FoldoutGroup("Parameters")]
        private void RenameParameters(string currentName, string newName)
        {
            StoryRegex.RenameParameter(this, currentName, newName);
            this.FindParameters();
        }

        [Button]
        public void ReplaceSymbolInRules(string oldSymbol, string newSymbol)
        {
            this.ReplaceSymbol(oldSymbol, newSymbol);
        }
    }
}