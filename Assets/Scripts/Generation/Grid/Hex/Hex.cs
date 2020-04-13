using System;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public struct Hex : ICoordinate, IEquatable<Hex>
    {
        public int Q { get; set; }
        public int R { get; set; }
        
        public int S => -this.Q - this.R;

        public int Length
        {
            get
            {
                var q = Mathf.Abs(this.Q);
                var r = Mathf.Abs(this.R);
                var s = Mathf.Abs(this.S);
                
                return (q + r + s) / 2;
            }
        }

        public Hex(int q, int r)
        {
            this.Q = q;
            this.R = r;
        }

        public IEnumerable<ICoordinate> GetNeighbors()
        {
            throw new NotImplementedException();
        }

        public int DistanceTo(Hex hex) => HexMath.Distance(this, hex);

        #region IEquatable

        public bool Equals(Hex other) => this.Q == other.Q && this.R == other.R;

        public override bool Equals(object obj) => obj is Hex other && this.Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Q * 397) ^ this.R;
            }
        }
        
        #endregion
        
        #region Operators
        
        public static explicit operator Hex(Vector3Int cube)
        {
            return new Hex
            {
                Q = cube.x,
                R = cube.z
            };
        }
        
        public static implicit operator Vector3Int(Hex axial)
        {
            return new Vector3Int(axial.Q, axial.S, axial.R);
        }

        public static bool operator ==(Hex x, Hex y) => x.Equals(y);

        public static bool operator !=(Hex x, Hex y) => !(x == y);

        public static Hex operator +(Hex x, Hex y)
        {
            return new Hex
            {
                Q = x.Q + y.Q,
                R = x.R + y.R
            };
        }
        
        public static Hex operator-(Hex x, Hex y)
        {
            return new Hex
            {
                Q = x.Q - y.Q,
                R = x.R - y.R
            };
        }

        public static Hex operator *(Hex x, Hex y)
        {
            return new Hex
            {
                Q = x.Q * y.Q,
                R = x.R * y.R
            };
        }
        
        #endregion
        
        public static readonly Hex Left = nw 
    }
}