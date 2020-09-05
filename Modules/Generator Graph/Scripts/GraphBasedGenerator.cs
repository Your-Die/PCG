using System;
using Chinchillada.Generation;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Chinchillada.GeneratorGraph
{
    [Serializable]
    public class GraphBasedGenerator<T> : GeneratorBase<T>
    {
        [SerializeField] private GeneratorGraph generatorGraph;

        [SerializeField] private bool specifyOutputName;

        [SerializeField, ShowIf(nameof(specifyOutputName))]
        private string outputName;

        protected override T GenerateInternal()
        {
            T result;

            var success = this.specifyOutputName
                ? this.generatorGraph.TryGetOutput(this.outputName, out result)
                : this.generatorGraph.TryGetOutput(out result);

            if (success)
                return result;
            
            var message =
                $"{nameof(GeneratorGraph)} {this.generatorGraph} did not contain an {nameof(IOutputNode)}" +
                $"with the name {this.outputName}.";
                
            throw new InvalidOperationException(message);
        }
    }
}