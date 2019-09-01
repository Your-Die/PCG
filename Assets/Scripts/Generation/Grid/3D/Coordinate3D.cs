
using UnityEngine;

namespace Chinchillada.Generation
{
    public class Coordinate3D : Coordinate2D
    {
        public int Z { get; set; }
        
        public override Vector3 ToVector() => new Vector3(this.X, this.Y, this.Z);
    }
}