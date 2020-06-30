using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada.Distributions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Chinchillada.Generation.Mazes
{
    public interface INodeSelector
    {
        GridNode SelectNode(IList<GridNode> nodes);
    }

    [Serializable]
    public class NewestSelector : INodeSelector
    {
        public GridNode SelectNode(IList<GridNode> nodes)
        {
            return nodes.Last();
        }
    }

    [Serializable]
    public class RandomSelector : INodeSelector
    {
        public GridNode SelectNode(IList<GridNode> nodes)
        {
            var index = Random.Range(0, nodes.Count);
            var node = nodes.ElementAt(index);

            return node;
        }
    }

    [Serializable]
    public class SelectorDistribution : INodeSelector
    {
        [SerializeField] private Dictionary<INodeSelector, int> selectors = new Dictionary<INodeSelector, int>();

        private IWeightedDistribution<INodeSelector> distribution;
            
        public GridNode SelectNode(IList<GridNode> nodes)
        {
            if (this.distribution == null) 
                this.BuildDistribution();

            var selector = this.distribution.Sample();
            return selector.SelectNode(nodes);
        }

        private void BuildDistribution()
        {
            this.distribution = this.selectors.ToWeighted();
        }
    }
}