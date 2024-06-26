using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.PCG.BSP
{
    public class BSPTreeGenerator : AsyncGeneratorComponentBase<BSPTree>
    {
        [SerializeField] private BoundsInt bounds;

        [SerializeField] private BoundsInt minimumPartitionSize;

        private readonly Queue<BSPTree> partitionQueue = new Queue<BSPTree>();

        public BSPTree GenerateTree(IRNG random) => this.GenerateAsync(random).Last();

        public override IEnumerable<BSPTree> GenerateAsync(IRNG random)
        {
            this.partitionQueue.Clear();

            var root = this.GenerateRoot();
            this.partitionQueue.Enqueue(root);

            while (this.partitionQueue.Any())
            {
                var node = this.partitionQueue.Dequeue();
                yield return node;

                this.Partition(node, random);
            }

            yield return root;
        }

        protected override BSPTree GenerateInternal(IRNG random) => this.GenerateTree(random);

        private void Partition(BSPTree node, IRNG random)
        {
            // Check how the node can be split.
            var canSplitHorizontal = node.Bounds.size.x > this.minimumPartitionSize.size.x * 2;
            var canSplitVertical = node.Bounds.size.y > this.minimumPartitionSize.size.y * 2;

            // Choose if both directions possible.
            if (canSplitHorizontal && canSplitVertical)
            {
                if (random.Flip())
                    this.PartitionHorizontal(node, random);
                else
                    this.PartitionVertical(node, random);
            }
            else if (canSplitHorizontal)
                this.PartitionHorizontal(node, random);
            else if (canSplitVertical)
                this.PartitionVertical(node, random);
        }

        /// <summary>
        /// Partitions the <paramref name="node"/> horizontally.
        /// </summary>
        private void PartitionHorizontal(BSPTree node, IRNG random)
        {
            // Calculate the range of possible partition points.
            var minimumSize = this.minimumPartitionSize.size.x;
            var min = node.Bounds.xMin + minimumSize;
            var max = node.Bounds.xMax - minimumSize;

            // Choose partition point.
            var partitionPoint = random.Range(min, max);

            var leftBounds = node.Bounds;
            var rightBounds = node.Bounds;
            
            leftBounds.xMax = partitionPoint;
            rightBounds.xMin = partitionPoint + 1;

            // Create new nodes and recurse.
            this.CreateChildren(node, leftBounds, rightBounds);
        }

        /// <summary>
        /// Partitions the <paramref name="node"/> vertically.
        /// </summary>
        private void PartitionVertical(BSPTree node, IRNG random)
        {
            // Calculate the range of possible partition points.
            var minimumSize = this.minimumPartitionSize.size.y;
            var min = node.Bounds.yMin + minimumSize;
            var max = node.Bounds.yMax - minimumSize;

            // Choose partition point.
            var partitionPoint = random.Range(min, max);
            
            var topBounds = node.Bounds;
            var bottomBounds = node.Bounds;
            
            topBounds.yMax = partitionPoint;
            bottomBounds.yMin = partitionPoint + 1;

            // Create new nodes and recurse.
            this.CreateChildren(node, topBounds, bottomBounds);
        }

        /// <summary>
        /// Creates two new child nodes of the <paramref name="parent"/>
        /// with the <paramref name="firstBounds"/> and <paramref name="secondBounds"/> respectively.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="firstBounds"></param>
        /// <param name="secondBounds"></param>
        private void CreateChildren(BSPTree parent, BoundsInt firstBounds, BoundsInt secondBounds)
        {
            // Create new nodes.
            var firstChild = new BSPTree(parent, firstBounds);
            var secondChild = new BSPTree(parent, secondBounds);

            // Assign to parent.
            parent.FirstChild = firstChild;
            parent.SecondChild = secondChild;

            // Partition them.
            this.partitionQueue.Enqueue(firstChild);
            this.partitionQueue.Enqueue(secondChild);
        }

        /// <summary>
        /// Generates a root <see cref="BSPTree"/> using the <see cref="bounds"/>.
        /// </summary>
        private BSPTree GenerateRoot() => new BSPTree(null, this.bounds);
    }
}