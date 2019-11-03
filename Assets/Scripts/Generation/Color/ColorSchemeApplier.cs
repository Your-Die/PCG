using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Colors
{
    [RequireComponent(typeof(Renderer))]
    public class ColorSchemeApplier : ChinchilladaBehaviour
    {
        [SerializeField] private int schemeIndex;

        [SerializeField, FindComponent] private Renderer rendererComponent;

        [SerializeField, FindComponent(SearchStrategy.InParent)]
        private IColorScheme scheme;

        public void ApplyScheme()
        {
            this.schemeIndex %= this.scheme.Count;
            var color = this.scheme[this.schemeIndex];

            this.rendererComponent.material.color = color;
        }

        protected override void Awake()
        {
            base.Awake();
            this.ApplyScheme();
        }
    }
}