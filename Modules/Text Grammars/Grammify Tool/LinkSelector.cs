using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Chinchillada.Foundation;
using Chinchillada.Foundation.UI;
using Mutiny.Foundation;
using Mutiny.Grammar;
using Mutiny.Thesis.UI;
using UnityEngine;

namespace Mutiny.Thesis.Tools
{
    public class LinkSelector : ChinchilladaBehaviour
    {
        [SerializeField] private OptionPresenter linkOptionsPresenter;

        [SerializeField] private RegexMatcher linkRegex;

        public event Action<GrammarRuleDefinition> LinkSelected;

        public void Present(string text, IGrammarDefinition definition)
        {
            var links = this.ExtractLinks(text, definition).Distinct();
            var options = links.Select(link => new Option<GrammarRuleDefinition>(link));
            this.linkOptionsPresenter.Present(options);
        }

        public void Hide()
        {
            this.linkOptionsPresenter.Hide();
        }

        private IEnumerable<GrammarRuleDefinition> ExtractLinks(string text, IGrammarDefinition definition)
        {
            var matches = this.linkRegex.Match(text);
            foreach (Match match in matches)
            {
                var group = match.Groups[1];
                var symbol = group.Value;

                if (!definition.Rules.TryFind(HasSymbol, out var rule))
                {
                    rule = new GrammarRuleDefinition(symbol, string.Empty);
                    definition.Rules.Add(rule);
                }

                yield return rule;

                bool HasSymbol(GrammarRuleDefinition ruleDefinition) => string.Equals(symbol, ruleDefinition.Symbol);
            }
        }

        private void OnOptionSelected(IOption option)
        {
            var ruleOption = (Option<GrammarRuleDefinition>) option;
            this.LinkSelected?.Invoke(ruleOption.Content);
        }

        private void OnEnable() => this.linkOptionsPresenter.SelectedEvent += this.OnOptionSelected;

        private void OnDisable() => this.linkOptionsPresenter.SelectedEvent -= this.OnOptionSelected;
    }
}