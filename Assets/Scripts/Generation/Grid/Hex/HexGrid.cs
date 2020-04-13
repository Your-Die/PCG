using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class HexGrid<T> : IGrid<T, Hex>
    {
        private Dictionary<Hex, T> cells = new Dictionary<Hex, T>();

        public int Count { get; }
        public int NeighborCount => 6;

        public T this[Hex coordinate]
        {
            get => this.cells[coordinate];
            set => this.cells[coordinate] = value;
        }

        public HexGrid(IEnumerable<Hex> hexes)
        {
            foreach (var hex in hexes)
                this.cells[hex] = default;
        }

        public IEnumerable<Hex> GetNeighbors(Hex coordinate) => coordinate.GetNeighbors();

        public IEnumerator<T> GetEnumerator() => this.cells.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public static HexGrid<T> Hexagon(int radius)
        {
            return new HexGrid<T>(Hexes());

            IEnumerable<Hex> Hexes()
            {
                for (var q = -radius; q <= radius; q++)
                {
                    var r1 = Math.Max(-radius, -q - radius);
                    var r2 = Math.Min(radius, -q + radius);

                    for (var r = r1; r <= r2; r++)
                        yield return new Hex(q, r);
                }
            }
        }

        public static HexGrid<T> Parallelogram(int q1, int q2, int r1, int r2)
        {
            return new HexGrid<T>(Hexes());

            IEnumerable<Hex> Hexes()
            {
                for (var q = q1; q <= q2; q++)
                for (var r = r1; r <= r2; r++)
                    yield return new Hex(q, r);
            }
        }

        public static HexGrid<T> Rectangle(int width, int height)
        {
            return new HexGrid<T>(Hexes());

            IEnumerable<Hex> Hexes()
            {
                for (var r = 0; r < height; r++)
                {
                    var offset = (int) Mathf.Floor(r / 2f);

                    for (var q = -offset; q < width - offset; q++)
                        yield return new Hex(q, r);
                }
            }
        }
    }
}