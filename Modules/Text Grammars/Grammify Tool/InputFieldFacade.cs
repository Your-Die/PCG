using Chinchillada.Foundation;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Mutiny.Thesis.Tools
{
    public class InputFieldFacade : ChinchilladaBehaviour
    {
        [SerializeField, Required, FindComponent(SearchStrategy.InChildren)]
        private TMP_InputField inputField;

        private int SelectionStart => this.inputField.selectionStringAnchorPosition;

        private int SelectionEnd => this.inputField.selectionStringFocusPosition;

        private bool HasSelection => this.SelectionStart != this.SelectionEnd;

        public string Text
        {
            get => this.inputField.text;
            set => this.inputField.text = value;
        }

        public void ReplaceSelectedText(string replacement)
        {
            if (!this.HasSelection)
                return;

            var (startPosition, endPosition) = this.GetSelectedTextBookends();
            
            var before = this.Text.Substring(0, startPosition);
            var after = this.Text.Substring(endPosition);

            this.Text = $"{before}{replacement}{after}";
        }

        public string GetSelectedText()
        {
            if (!this.HasSelection)
                return null;

            var (startPosition, endPosition) = this.GetSelectedTextBookends();
            var length = endPosition - startPosition;

            return this.inputField.text.Substring(startPosition, length);
        }

        private (int selectionStart, int selectionEnd) GetSelectedTextBookends()
        {
            var startPosition = this.SelectionStart;
            var endPosition = this.SelectionEnd;

            // Sort start and end.
            if (startPosition > endPosition)
            {
                var temp = startPosition;
                startPosition = endPosition;
                endPosition = temp;
            }

            return (startPosition, endPosition);
        }
    }
}