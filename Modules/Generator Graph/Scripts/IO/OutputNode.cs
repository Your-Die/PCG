using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace Chinchillada.GeneratorGraph
{
    public interface IOutputNode : INamedNode
    {
        IOutputGetter BuildGetter();
    }

    public interface IOutputGetter : INodeWrapper
    {
        IOutputNode OutputNode { get; }
    }

    public class OutputNode<T> : Node, IOutputNode
    {
        [SerializeField] private string outputName;

        [SerializeField, Input] private T value;

        public string Name => this.outputName;
        
        public IOutputGetter BuildGetter() => new Getter(this);

        public T Get() => this.value = this.GetInputValue(nameof(this.value), this.value);

        [Serializable]
        public class Getter : IOutputGetter
        {
            [SerializeField, ReadOnly, UsedImplicitly] private string name;

            [SerializeField, ReadOnly, UsedImplicitly] private T value;

            [SerializeField, ReadOnly] private OutputNode<T> node;

            INamedNode INodeWrapper.Node => this.node;
            public IOutputNode OutputNode => this.node;
            
            public Getter()
            {
            }

            public Getter(OutputNode<T> node)
            {
                this.node = node;
                this.Read();
            }

            private void Read()
            {
                this.name = this.node.Name;
                this.value = this.node.value;
            }

            public T Get() => this.node.Get();
        }
    }
}