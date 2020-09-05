using System.Collections.Generic;
using Chinchillada.Foundation;
using Chinchillada.Foundation.States;
using Mutiny.Grammar;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Mutiny.Thesis.Tools
{
    public class GrammifyController : ChinchilladaBehaviour
    {
        [SerializeField] private IGrammarDefinition grammarDefinition;

        [SerializeField] private TMP_InputField titleTextElement;

        [SerializeField] private IInvokableEvent grammarSavedEvent;
        
        [FoldoutGroup("Input Fields"), SerializeField, FindComponent(SearchStrategy.InChildren), Required]
        private InputFieldFacade grammarInput;

        [FoldoutGroup("Input Fields"), SerializeField]
        private TMP_InputField symbolInput;

        [FoldoutGroup("Selectors"), SerializeField]
        private RuleSelector ruleSelector;

        [FoldoutGroup("Selectors"), SerializeField]
        private LinkSelector linkSelector;


        [SerializeField, Required, FoldoutGroup("Buttons")]
        private Button grammifyButton;

        [SerializeField, Required, FoldoutGroup("Buttons")]
        private Button goBackButton;

        [SerializeField, Required, FoldoutGroup("Buttons")]
        private Button saveButton;

        [SerializeField, Required, FoldoutGroup("Buttons")]
        private Button addReplacementButton;
        
        [SerializeField, Required, FoldoutGroup("Buttons")]
        private Button deleteReplacementButton;


        private string selectedText;

        private readonly Stack<IRuleItem> ruleStack = new Stack<IRuleItem>();

        private readonly StateMachine stateMachine = new StateMachine();

        [Button]
        public void GrammifySelection()
        {
            this.SaveText();

            this.selectedText = this.grammarInput.GetSelectedText();
            this.stateMachine.TransitionTo(new WriteSymbolState(this, this.selectedText));
        }

        public void SubmitSymbol(string symbol)
        {
            this.grammarInput.ReplaceSelectedText($"#{symbol}#");
            var ruleItem = this.CreateRuleItem(symbol, this.selectedText);

            this.SaveAndPush(ruleItem);

            this.stateMachine.TransitionTo(new WriteTextState(this));
        }

        private void SaveText()
        {
            var ruleItem = this.ruleStack.Peek();
            ruleItem.Replacement = this.grammarInput.Text;
            
            this.UpdateLinks();
            this.grammarSavedEvent.Invoke();
        }

        private void LoadText()
        {
            var ruleItem = this.ruleStack.Peek();

            this.grammarInput.Text = ruleItem.Replacement;
            this.titleTextElement.text = ruleItem.Rule.Symbol;

            this.UpdateLinks();
        }

        private void AddReplacement()
        {
            if (this.ruleStack.Count == 1)
            {
                Debug.LogWarning("We cannot add another replacement to the origin rule.");
                return;
            }

            this.SaveText();
            
            var ruleItem = this.ruleStack.Peek();
            var replacementList = ruleItem.Rule.Replacements;

            var index = replacementList.Count;
            replacementList.Add(string.Empty);

            this.SetReplacement(index);
        }

        private void DeleteReplacement()
        {
            if (this.ruleStack.Count == 1) // origin node.
            {
                Debug.LogWarning("We cannot delete the replacement for the origin rule.");
                return;
            }

            var ruleItem = this.ruleStack.Peek();

            var rule = ruleItem.Rule;
            var index = ruleItem.ReplacementIndex;

            var replacementList = rule.Replacements;
            replacementList.RemoveAt(index);

            if (replacementList.Count == 0)
            {
                // Remove the empty rule from stack and definition.
                this.ruleStack.Pop();
                this.grammarDefinition.Rules.Remove(rule);

                // load text for the new head of the stack.
                this.LoadText();
            }
            else
            {
                if (index == replacementList.Count)
                    index -= 1;

                this.SetReplacement(index);
            }
        }

        private void SwitchToReplacement(int index)
        {
            this.SaveText();
            this.SetReplacement(index);
        }
        
        private void SetReplacement(int index)
        {
            var item = this.ruleStack.Pop();
            item.ReplacementIndex = index;

            this.PushRule(item);
        }

        private void PushRule(GrammarRuleDefinition rule)
        {
            var item = new RuleItem(rule, 0);
            this.PushRule(item);
        }

        private void SaveAndPush(IRuleItem item)
        {
            this.SaveText();
            this.PushRule(item);
        }


        private void PushRule(IRuleItem item)
        {
            this.ruleStack.Push(item);
            this.LoadText();
        }

        private void PopRule()
        {
            if (this.ruleStack.Count <= 1)
            {
                Debug.LogWarning("Can't go back further than the origin.");
                return;
            }

            this.SaveText();

            this.ruleStack.Pop();

            this.LoadText();
        }

        [Button]
        private void UpdateLinks()
        {
            var currentItem = this.ruleStack.Peek();

            this.linkSelector.Present(currentItem.Replacement, this.grammarDefinition);
            this.ruleSelector.Present(currentItem.Rule);

            this.ruleSelector.SetHighlightedIndex(currentItem.ReplacementIndex);
        }

        private RuleItem CreateRuleItem(string symbol, string replacement)
        {
            int replacementIndex;

            if (this.grammarDefinition.Rules.TryFind(MatchRule, out var rule))
            {
                replacementIndex = rule.Replacements.IndexOf(replacement);

                if (replacementIndex == -1)
                {
                    replacementIndex = rule.Replacements.Count;
                    rule.Replacements.Add(replacement);
                }
            }
            else
            {
                rule = new GrammarRuleDefinition(symbol, replacement);
                this.grammarDefinition.Rules.Add(rule);

                replacementIndex = 0;
            }

            return new RuleItem(rule, replacementIndex);

            bool MatchRule(GrammarRuleDefinition matchingRule) => string.Equals(symbol, matchingRule.Symbol);
        }

        private void Start()
        {
            base.Awake();

            var item = new OriginItem(this.grammarDefinition);
            this.ruleStack.Push(item);
            this.LoadText();
        }

        private void OnEnable() => this.stateMachine.TransitionTo(new WriteTextState(this));

        private void OnDisable() => this.stateMachine.Exit();

        private class WriteTextState : StateBase
        {
            private readonly GrammifyController controller;

            private readonly Button grammifyButton;
            private readonly Button goBackButton;
            private readonly Button saveButton;
            private readonly Button addReplacementButton;
            private readonly Button deleteReplacementButton;

            private readonly RuleSelector ruleSelector;
            private readonly LinkSelector linkSelector;

            public WriteTextState(GrammifyController controller)
            {
                this.controller = controller;

                this.grammifyButton = this.controller.grammifyButton;
                this.goBackButton = this.controller.goBackButton;
                this.saveButton = this.controller.saveButton;
                this.addReplacementButton = this.controller.addReplacementButton;
                this.deleteReplacementButton = this.controller.deleteReplacementButton;

                this.ruleSelector = this.controller.ruleSelector;
                this.linkSelector = this.controller.linkSelector;
            }

            public override void Enter()
            {
                base.Enter();

                this.grammifyButton.onClick.AddListener(this.controller.GrammifySelection);
                this.goBackButton.onClick.AddListener(this.controller.PopRule);
                this.saveButton.onClick.AddListener(this.controller.SaveText);
                this.addReplacementButton.onClick.AddListener(this.controller.AddReplacement);
                this.deleteReplacementButton.onClick.AddListener(this.controller.DeleteReplacement);

                this.ruleSelector.ReplacementSelected += this.controller.SwitchToReplacement;
                this.linkSelector.LinkSelected += this.controller.PushRule;

                this.controller.titleTextElement.onEndEdit.AddListener(this.controller.UpdateSymbol);

                EventSystem.current.SetSelectedGameObject(this.grammifyButton.gameObject);
            }

            public override void Exit()
            {
                base.Exit();
                this.grammifyButton.onClick.RemoveListener(this.controller.GrammifySelection);
                this.goBackButton.onClick.RemoveListener(this.controller.PopRule);
                this.saveButton.onClick.RemoveListener(this.controller.SaveText);
                this.addReplacementButton.onClick.RemoveListener(this.controller.AddReplacement);
                this.deleteReplacementButton.onClick.RemoveListener(this.controller.DeleteReplacement);

                this.controller.titleTextElement.onEndEdit.RemoveListener(this.controller.UpdateSymbol);

                this.ruleSelector.ReplacementSelected -= this.controller.SwitchToReplacement;
                this.linkSelector.LinkSelected -= this.controller.PushRule;
            }
        }

        private void UpdateSymbol(string symbol)
        {
            var currentItem = this.ruleStack.Peek();
            var rule = currentItem.Rule;

            this.grammarDefinition.ReplaceSymbol(rule, symbol);
            this.LoadText();
        }

        private class WriteSymbolState : StateBase
        {
            private readonly GrammifyController controller;
            private readonly string text;

            private readonly TMP_InputField symbolInput;

            public WriteSymbolState(GrammifyController controller, string text)
            {
                this.controller = controller;
                this.text = text;
                this.symbolInput = controller.symbolInput;
            }

            public override void Enter()
            {
                base.Enter();

                var inputObject = this.symbolInput.gameObject;
                inputObject.SetActive(true);

                EventSystem.current.SetSelectedGameObject(inputObject);

                this.symbolInput.text = this.text;
                this.symbolInput.onEndEdit.AddListener(this.OnEndEdit);
            }

            public override void Exit()
            {
                base.Exit();

                this.symbolInput.onEndEdit.RemoveListener(this.OnEndEdit);
                this.symbolInput.gameObject.SetActive(false);
            }

            private void OnEndEdit(string symbolText) => this.controller.SubmitSymbol(symbolText);
        }

        private interface IRuleItem
        {
            GrammarRuleDefinition Rule { get; }

            int ReplacementIndex { get; set; }

            string Replacement { get; set; }
        }

        private class RuleItem : IRuleItem
        {
            public virtual GrammarRuleDefinition Rule { get; }

            public int ReplacementIndex { get; set; }

            public RuleItem(GrammarRuleDefinition rule, int replacementIndex)
            {
                this.Rule = rule;
                this.ReplacementIndex = replacementIndex;
            }

            public string Replacement
            {
                get => this.Rule.Replacements[this.ReplacementIndex];
                set => this.Rule.Replacements[this.ReplacementIndex] = value;
            }

            public override string ToString() => $"{this.Rule}: {this.ReplacementIndex}";
        }

        private class OriginItem : IRuleItem
        {
            private readonly IGrammarDefinition grammarDefinition;

            public OriginItem(IGrammarDefinition grammarDefinition)
            {
                this.grammarDefinition = grammarDefinition;
            }

            public GrammarRuleDefinition Rule => this.grammarDefinition.BuildOriginRule();

            public int ReplacementIndex
            {
                get => 0;
                set { }
            }

            public string Replacement
            {
                get => this.grammarDefinition.Origin;
                set => this.grammarDefinition.Origin = value;
            }

            public override string ToString() => this.Rule.ToString();
        }
    }
}