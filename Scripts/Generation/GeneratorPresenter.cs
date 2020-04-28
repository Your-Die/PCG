using Chinchillada;
using Chinchillada.Generation;
using Chinchillada.Utilities;
using Sirenix.OdinInspector;

namespace UnityEngine
{
    public abstract class GeneratorPresenter<T> : ChinchilladaBehaviour
    {
        [SerializeField, Required, FindComponent(SearchStrategy.InChildren)]
        private IGenerator<T> generator;

        private void OnEnable() => this.generator.Generated += Present;

        private void OnDisable() => this.generator.Generated -= this.Present;

        public abstract void Present(T content);
    }
}