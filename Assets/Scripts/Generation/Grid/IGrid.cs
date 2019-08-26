using System.Collections.Generic;

namespace Chinchillada
{
    public interface IGrid<out TCoordinates>
    {
        IEnumerable<TCoordinates> GetCoordinates();

        IGrid<TCoordinates> Copy();
    }
}