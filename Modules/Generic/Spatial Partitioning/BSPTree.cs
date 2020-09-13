using System;
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

        public BSPTree FindPartition(Vector3Int cell)
        {
            if (this.IsLeafNode)
            {
                if (this.Bounds.Contains(cell))
                    return this;
                
                throw new InvalidOperationException();
            }

            var child = this.FirstChild.Bounds.Contains(cell)
                ? this.FirstChild
                : this.SecondChild;

            return child.FindPartition(cell);
        }

        public void PartitionHorizontal(int partitionPoint)
        {
            var leftBounds = this.Bounds;
            var rightBounds = this.Bounds;

            leftBounds.xMax = partitionPoint;
            rightBounds.xMin = partitionPoint;

            this.FirstChild = new BSPTree(this, leftBounds);
            this.SecondChild = new BSPTree(this, rightBounds);
        }

        public void PartitionVertical(int partitionPoint)
        {
            var topBounds = this.Bounds;
            var bottomBounds = this.Bounds;
            
            bottomBounds.yMax = partitionPoint;
            topBounds.yMin = partitionPoint;
            
            this.FirstChild = new BSPTree(this, bottomBounds);
            this.SecondChild = new BSPTree(this, topBounds);
        }
    }
}