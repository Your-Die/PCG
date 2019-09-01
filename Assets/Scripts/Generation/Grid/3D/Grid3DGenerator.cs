using Chinchillada.Distributions;
using Chinchillada.Utilities;

namespace Chinchillada.Generation
{
    public class Grid3DGenerator : IGenerator<Grid3D>
    {
        private readonly int width;
        private readonly int height;
        private readonly int depth;

        private IDistribution<bool> fillDistribution;

        public Grid3DGenerator(int width, int height, int depth, float fillPercentage)
            : this(width, height, depth, Flip.Boolean(fillPercentage))
        {
        }

        public Grid3DGenerator(int width, int height, int depth, IDistribution<bool> fillDistribution)
        {
            this.width = width;
            this.height = height;
            this.depth = depth;

            this.fillDistribution = fillDistribution;
        }

        public Grid3D Generate()
        {
            var items = new int[this.width, this.height, this.depth];

            for (var x = 0; x < this.width; x++)
            for (var y = 0; y < this.height; y++)
            for (var z = 0; z < this.depth; z++)
                items[x, y, z] = this.fillDistribution.Sample().AsBinary();

            return new Grid3D(items);
        }
    }
}