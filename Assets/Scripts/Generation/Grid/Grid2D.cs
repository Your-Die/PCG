using System.Collections.Generic;

namespace Chinchillada.Generation.CellularAutomata
{
    public class Grid2D
    {
        public int[,] Items { get; }

        public int Width { get; }

        public int Height { get; }

        public Grid2D(int[,] items)
        {
            this.Width = items.GetLength(0);
            this.Height = items.GetLength(1);
            
            this.Items = items;
        }

        public Grid2D(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            
            this.Items = new int[width, height];
        }

        public IEnumerable<Coordinate2D> GetCoordinates()
        {
            for (var x = 0; x < this.Width; x++)
            for (var y = 0; y < this.Height; y++)
            {
                yield return new Coordinate2D {X = x, Y = y};
            }
        }
    }
}