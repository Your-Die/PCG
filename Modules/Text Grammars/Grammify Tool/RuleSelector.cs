using System;
using Chinchillada.Foundation;
using Chinchillada.Foundation.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Mutiny.Thesis.Tools
{
    public class RuleSelector : SubscriberBehaviour, IPresenter<GrammarRuleDefinition>
    {
        [SerializeField] private PoolingList<ReplacementPresenter> replacementPresenters;

        [SerializeField] private Highlighter highlighter;

        [FoldoutGroup("Buttons"), SerializeField]
        private Button previousReplacementButton;

        [FoldoutGroup("Buttons"), SerializeField]
        private Button nextReplacementButton;

        private int selectedIndex;

        public event Action<int> ReplacementSelected;

        public void SetHighlightedIndex(int index)
        {
            this.selectedIndex = index;

            var presenter = this.replacementPresenters[index];
            var button = presenter.Button;

            this.highlighter?.Highlight(button.transform);
        }

        public void Present(GrammarRuleDefinition content)
        {
            this.replacementPresenters.ApplyWith(content.Replacements, Present);
            this.Subscribe();

            void Present(int index, string replacement, ReplacementPresenter presenter)
            {
                presenter.Present(replacement, index);
            }
        }

        public void Hide()
        {
            this.replacementPresenters.Clear();
            this.highlighter?.Hide();

            this.Unsubscribe();
        }

        protected override void Awake()
        {
            base.Awake();

            this.replacementPresenters.ItemActivated += this.OnItemActivated;
            this.replacementPresenters.ItemDeactivated += this.OnItemDeactivated;
        }
        
        protected override void ActivateSubscriptions()
        {
            this.previousReplacementButton.onClick.AddListener(this.SelectPreviousReplacement);
            this.nextReplacementButton.onClick.AddListener(this.SelectNextReplacement);
        }

        protected override void DeactivateSubscriptions()
        {
            this.previousReplacementButton.onClick.RemoveListener(this.SelectPreviousReplacement);
            this.nextReplacementButton.onClick.RemoveListener(this.SelectNextReplacement);
        }
        
        private void SelectPreviousReplacement()
        {
            this.selectedIndex--;
            if (this.selectedIndex < 0)
                this.selectedIndex = this.replacementPresenters.Count - 1;

            this.OnReplacementSelected(this.selectedIndex);
        }

        private void SelectNextReplacement()
        {
            this.selectedIndex++;
            if (this.selectedIndex >= this.replacementPresenters.Count)
                this.selectedIndex = 0;

            this.OnReplacementSelected(this.selectedIndex);
        }

        private void OnReplacementSelected(int index) => this.ReplacementSelected?.Invoke(index);

        private void OnItemActivated(ReplacementPresenter presenter)
        {
            presenter.Selected += this.OnReplacementSelected;
        }

        private void OnItemDeactivated(ReplacementPresenter presenter)
        {
            presenter.Selected -= this.OnReplacementSelected;
        }
    }
}