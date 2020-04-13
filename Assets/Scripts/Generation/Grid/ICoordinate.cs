using System.Collections.Generic;

namespace Chinchillada.Generation.Grid
{
    public interface ICoordinate
    {
        IEnumerable<ICoordinate> GetNeighbors();
    }
}