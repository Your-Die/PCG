using System.Collections.Generic;

namespace Chinchillada.Generation.CellularAutomata
{
    public interface IGrid<out TCoordinates>
    {
        IEnumerable<TCoordinates> GetCoordinates();

        IGrid<TCoordinates> Copy();
    }
}