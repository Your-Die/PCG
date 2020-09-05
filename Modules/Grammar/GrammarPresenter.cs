using System.Collections.Generic;
using System.Linq;
using Chinchillada.Foundation;
using UnityEngine;
using UnityEngine.UI;

namespace Chinchillada.Generation.Grammar
{
    public class GrammarPresenter : ChinchilladaBehaviour, IRenderer<IEnumerable<Symbol>>
    {
        [SerializeField] private Text textElement;

        public IEnumerable<Symbol> Content { get; private set; }
        public void Render(IEnumerable<Symbol> content)
        {
            this.Content = content.ToList();
            
            var text = string.Join(", ", this.Content);
            this.textElement.text = text;
        }

        public void Hide()
        {
            this.textElement.text = string.Empty;
        }

    }
}