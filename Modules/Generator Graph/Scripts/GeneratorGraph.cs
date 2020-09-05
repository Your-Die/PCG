using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace Chinchillada.GeneratorGraph
{
    [CreateAssetMenu]
    public class GeneratorGraph : NodeGraph
    {
        [SerializeField, InlineEditor] private List<IInputSetter> inputs = new List<IInputSetter>();
        [SerializeField, InlineEditor] private List<IOutputGetter> outputs = new List<IOutputGetter>();

        private readonly Dictionary<string, IInputNode> inputDictionary = new Dictionary<string, IInputNode>();
        private readonly Dictionary<string, IOutputNode> outputDictionary = new Dictionary<string, IOutputNode>();
        
        public bool SetInput<T>(string inputName, T value)
        {
            if (!this.inputDictionary.TryGetValue(inputName, out var node))
                return false;

            if (!(node is InputNode<T> inputNode))
                return false;

            inputNode.Value = value;
            return true;
        }
        
        public bool TryGetOutput<T>(string outputName, out T value)
        {
            value = default;
            
            if (!this.outputDictionary.TryGetValue(outputName, out var node))
                return false;

            if (!(node is OutputNode<T> outputNode))
                return false;

            value = outputNode.Get();
            return true;
        }

        public bool TryGetOutput<T>(out T value)
        {
            foreach (var node in this.outputDictionary.Values)
            {
                if (!(node is OutputNode<T> outputNode)) 
                    continue;
                
                value = outputNode.Get();
                return true;
            }

            value = default;
            return false;
        }

        private void OnEnable()
        {
            this.UpdateNodes();

            var inputNodes = this.inputs.Select(setter => setter.InputNode);
            var outputNodes = this.outputs.Select(getter => getter.OutputNode);

            BuildDictionary(inputNodes, this.inputDictionary);
            BuildDictionary(outputNodes, this.outputDictionary);
            
            void BuildDictionary<T>(IEnumerable<T> namedNodes, IDictionary<string, T> dictionary) 
                where T : INamedNode
            {
                dictionary.Clear();

                foreach (var node in namedNodes) 
                    dictionary[node.Name] = node;
            }
        }

        [Button]
        private void UpdateNodes()
        {
            var newInputs = new List<IInputNode>();
            var newOutputs = new List<IOutputNode>();

            foreach (var node in this.nodes)
            {
                switch (node)
                {
                    case IInputNode inputNode:
                        newInputs.Add(inputNode);
                        break;
                    case IOutputNode outputNode:
                        newOutputs.Add(outputNode);
                        break;
                }
            }

            UpdateSetters(newInputs);
            UpdateGetters(newOutputs);

            void UpdateGetters(IEnumerable<IOutputNode> outputNodes)
            {
                if (this.outputs == null)
                    this.outputs = new List<IOutputGetter>();

                this.outputs.RemoveAll(setter => !outputNodes.Contains(setter.Node));
                var newNodes = outputNodes.Where(IsNew<IOutputNode>(this.outputs)).ToArray();

                foreach (var node in newNodes)
                {
                    var setter = node.BuildGetter();
                    this.outputs.Add(setter);
                }
            }

            void UpdateSetters(IEnumerable<IInputNode> inputNodes)
            {
                if (this.inputs == null)
                    this.inputs = new List<IInputSetter>();

                this.inputs.RemoveAll(setter => !inputNodes.Contains(setter.Node));
                var newNodes = inputNodes.Where(IsNew<IInputNode>(this.inputs)).ToArray();

                foreach (var node in newNodes)
                {
                    var setter = node.BuildSetter();
                    this.inputs.Add(setter);
                }
            }
            
            Func<T, bool> IsNew<T>(IEnumerable<INodeWrapper> wrappers) where T : INamedNode
            {
                return node => wrappers.All(setter => setter.Node != (INamedNode) node);
            }
        }
    }
}