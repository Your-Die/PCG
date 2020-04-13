using TMPro;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class TextRenderer : ChinchilladaBehaviour, IRenderer<string>
    {
        [SerializeField] private TMP_Text textField;
        
        public void Render(string content)
        {
            this.textField.text = content;
        }
    }
}