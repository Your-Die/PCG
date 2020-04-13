using System;

namespace Chinchillada.Generation.Grid
{
    public interface IGridRenderer
    {
        IntGrid2D Grid { get; }

        event Action<IntGrid2D> NewGridRegistered;


        /// <summary>
        /// Render the <paramref name="grid"/>.
        /// </summary>
        void Render(IntGrid2D grid);
    }
}

