using System;
using System.Collections.Generic;
using System.Linq;
using Chinchillada;
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
            if (!this.Bounds.Contains(cell))
                return null;

            if (this.IsLeafNode)
                return this;

            var child = this.FirstChild.Bounds.Contains(cell)
                ? this.FirstChild
                : this.SecondChild;

            return child.FindPartition(cell);
        }

        public IEnumerable<BSPTree> GetLeaves()
        {
            if (this.IsLeafNode)
                return Enumerables.Single(this);

            var firstLeaves = this.FirstChild.GetLeaves();
            var secondLeaves = this.SecondChild.GetLeaves();

            return firstLeaves.Concat(secondLeaves);
        }

        public (BSPTree FirstChild, BSPTree SecondChild) PartitionHorizontal(int partitionPoint)
        {
            var leftBounds = this.Bounds;
            var rightBounds = this.Bounds;

            leftBounds.xMax = partitionPoint;
            rightBounds.xMin = partitionPoint;

            this.FirstChild = new BSPTree(this, leftBounds);
            this.SecondChild = new BSPTree(this, rightBounds);

            return (this.FirstChild, this.SecondChild);
        }

        public (BSPTree FirstChild, BSPTree SecondChild) PartitionVertical(int partitionPoint)
        {
            var topBounds = this.Bounds;
            var bottomBounds = this.Bounds;

            bottomBounds.yMax = partitionPoint;
            topBounds.yMin = partitionPoint;

            this.FirstChild = new BSPTree(this, bottomBounds);
            this.SecondChild = new BSPTree(this, topBounds);

            return (this.FirstChild, this.SecondChild);
        }

        public void ResetChildren()
        {
            this.FirstChild = null;
            this.SecondChild = null;
        }
    }
}