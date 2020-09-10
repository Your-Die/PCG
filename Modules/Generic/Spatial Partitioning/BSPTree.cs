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
                return this;

            var child = this.FirstChild.Bounds.Contains(cell)
                ? this.FirstChild
                : this.SecondChild;

            return child.FindPartition(cell);
        }

        public static (BSPTree firstChild, BSPTree secondChild) PartitionHorizontal(BSPTree tree, int partitionPoint)
        {
            var leftBounds = tree.Bounds;
            var rightBounds = tree.Bounds;

            leftBounds.xMax = partitionPoint;
            rightBounds.xMin = partitionPoint;

            var leftChild = new BSPTree(tree, leftBounds);
            var rightChild = new BSPTree(tree, rightBounds);

            return (leftChild, rightChild);
        }

        public static (BSPTree firstChild, BSPTree secondChild) PartitionVertical(BSPTree tree, int partitionPoint)
        {
            var topBounds = tree.Bounds;
            var bottomBounds = tree.Bounds;
            
            topBounds.yMax = partitionPoint;
            bottomBounds.yMin = partitionPoint;
            
            var topChild = new BSPTree(tree, topBounds);
            var bottomChild = new BSPTree(tree, bottomBounds);

            return (topChild, bottomChild);
        }
    }
}