using Chinchillada.Distributions;
using Chinchillada.Utilities;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// Generates random instances of <see cref="Grid3D"/>.
    /// </summary>
    public class Grid3DGenerator : IGenerator<Grid3D>
    {
        /// <summary>
        /// The width of the generated grid.
        /// </summary>
        private readonly int width;
        
        /// <summary>
        /// The height of the generated grid.
        /// </summary>
        private readonly int height;
        
        /// <summary>
        /// The depth of the generated grid.
        /// </summary>
        private readonly int depth;

        /// <summary>
        /// The distribution used for filling the cells on the grid.
        /// </summary>
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

        /// <summary>
        /// Generate a <see cref="Grid3D"/>.
        /// </summary>
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