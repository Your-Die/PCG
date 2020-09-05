using Chinchillada.Foundation;
using TMPro;
using UnityEngine;

namespace Chinchillada.Grammar
{
    public class TraceryPreview : ChinchilladaBehaviour
    {
        [SerializeField] private TMP_Text textElement;
        
        [SerializeField] private TraceryTextGenerator generator;

        [SerializeField] private IEvent generateEvent;
        
        public void GeneratePreview()
        {
            var text = this.generator.Generate();
            this.textElement.text = text;
        }
        
        private void OnEnable() => this.generateEvent.Subscribe(this.GeneratePreview);

        private void OnDisable() => this.generateEvent.Unsubscribe(this.GeneratePreview);
    }
}