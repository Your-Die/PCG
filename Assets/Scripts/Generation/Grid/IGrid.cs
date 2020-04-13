using System.Collections.Generic;

namespace Chinchillada.Generation.Grid
{
    public interface IGrid<TContent, TCoordinate> : IReadOnlyCollection<TContent>
    {
        int NeighborCount { get; }
        
        TContent this[TCoordinate coordinate] { get; set; }

        IEnumerable<TCoordinate> GetNeighbors(TCoordinate coordinate);
    }
}