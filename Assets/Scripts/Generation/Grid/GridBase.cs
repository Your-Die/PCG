using System.Collections;
using System.Collections.Generic;

namespace Chinchillada.Generation.Grid
{
    public abstract class GridBase<TContent, TCoordinate> : IGrid<TContent, TCoordinate>
    {
        private readonly Dictionary<TCoordinate, TContent> cells = new Dictionary<TCoordinate, TContent>();

        public int Count => this.cells.Count;
        public abstract int NeighborCount { get; }

        public TContent this[TCoordinate coordinate]
        {
            get => this.cells[coordinate];
            set => this.cells[coordinate] = value;
        }

        public abstract IEnumerable<TCoordinate> GetNeighbors(TCoordinate coordinate);

        public IEnumerator<TContent> GetEnumerator() => this.cells.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

}