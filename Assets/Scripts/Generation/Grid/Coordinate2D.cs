using UnityEngine;

namespace Chinchillada.Generation
{
    public class Coordinate2D
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Get(Grid2D grid) => grid.Items[this.X, this.Y];

        public void Set(int value, Grid2D grid) => grid.Items[this.X, this.Y] = value;

        public Vector3 ToVector() => new Vector3(this.X, this.Y);
    }
}