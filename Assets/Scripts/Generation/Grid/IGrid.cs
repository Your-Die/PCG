using System;
using System.Collections.Generic;
using Chinchillada.Generation;
using Generation.Grid;

namespace Chinchillada
{
    public interface IGrid<TCoordinates> : IGrid
    {
        IEnumerable<TCoordinates> GetCoordinates();

        INeighborhood GetNeighborhood(TCoordinates coordinate, int radius);
        IGrid<TCoordinates> CopyShape();
    }

    public interface IGrid
    {
        void ForEach(Action<ICoordinate, int> action);

        IGrid Select(Func<int, int> selector);

        IGrid SelectNeighborhood(int radius, Func<INeighborhood, int> selector);
        
        IEnumerable<ICoordinate> GetCoordinates();

        IGrid CopyShape();
    }
}