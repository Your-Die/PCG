
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class Coordinate3D : Coordinate2D
    {
        public int Z { get; set; }

        int Get(Grid3D grid) => grid[this];

        void Set(Grid3D grid, int value) => grid[this] = value;
        
        public override Vector3 ToVector() => new Vector3(this.X, this.Y, this.Z);

        public override string ToString() => $"({this.X}, {this.Y}, {this.Z})";
    }
}