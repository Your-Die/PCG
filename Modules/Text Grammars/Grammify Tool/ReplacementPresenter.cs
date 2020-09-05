using System;
using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mutiny.Thesis.Tools
{
    public class ReplacementPresenter : ChinchilladaBehaviour
    {
        [SerializeField, Required, FindComponent(SearchStrategy.InChildren)]
        private TMP_Text textElement;

        [SerializeField, Required, FindComponent(SearchStrategy.InChildren)]
        private Button button;
        
        public int Index { get; private set; }

        public Button Button => this.button;

        public event Action<int> Selected;

        public void Present(string text, int index)
        {
            this.textElement.text = text;
            this.Index = index;

            this.Button.onClick.AddListener(this.OnButtonClicked);
        }

        public void Hide()
        {
            this.textElement.text = string.Empty;
            this.Index = -1;

            this.Button.onClick.RemoveListener(this.OnButtonClicked);
        }

        private void OnButtonClicked() => this.Selected?.Invoke(this.Index);
    }
}