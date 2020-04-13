using System;
using System.Collections;
using System.Collections.Generic;
using Chinchillada.Utilities;
using UnityEngine;

namespace Chinchillada.Generation.Grid
{
    public class HexGrid<T> : IGrid<T, Hex>
    {
        private T[][] cells;

        public int Width { get; }
        public int Height { get; }

        public int NeighborCount => 6;

        public T this[Hex coordinate]
        {
            get => this.cells[coordinate.Q][coordinate.R];
            set => this.cells[coordinate.Q][coordinate.R] = value;
        }

        public HexGrid(int size)
        {
            if (size.IsEven())
                throw new ArgumentException();

     
        }

        public IEnumerable<Hex> GetNeighbors(Hex coordinate)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count { get; }
    }
}