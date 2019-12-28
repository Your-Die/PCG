using Chinchillada.Distributions;
using Chinchillada.Utilities;

namespace Chinchillada.Generation.Grid
{
    public class Grid2DGenerator : IGenerator<Grid2D>
    {
        private readonly int width;
        private readonly int height;
        private IDistribution<int> valueDistribution;

        public Grid2DGenerator(int width, int height, float flipPercentage)
            : this(width, height, Flip.Binary(flipPercentage))
        {
        }

        public Grid2DGenerator(int width, int height, IDistribution<int> valueDistribution)
        {
            this.width = width;
            this.height = height;

            this.valueDistribution = valueDistribution;
        }

        public Grid2D Generate()
        {
            var items = new int[this.width, this.height];
            
            for (var x = 0; x < this.width; x++)
            for (var y = 0; y < this.height; y++)
                items[x, y] = this.valueDistribution.Sample();
            
            return new Grid2D(items);
        }
    }
}