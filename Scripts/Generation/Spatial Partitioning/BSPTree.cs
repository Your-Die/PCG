using UnityEngine;

namespace Chinchillada.Generation.BSP
{
    public class BSPTree
    {
        public BSPTree Parent { get; }

        public BoundsInt Bounds { get; }

        public BSPTree FirstChild { get; set; }
        
        public BSPTree SecondChild { get; set; }

        public bool IsLeafNode => this.FirstChild == null && this.SecondChild == null;

        public BSPTree(BSPTree parent, BoundsInt bounds)
        {
            this.Parent = parent;
            this.Bounds = bounds;
        }
    }
}