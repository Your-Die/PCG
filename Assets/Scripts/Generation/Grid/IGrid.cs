using System;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// Interface for the genericly typed version of <see cref="IGrid"/>, which is typed on the type of <see cref="ICoordinate"/>
    /// That can be used to index on the grid.
    /// </summary>
    /// <typeparam name="TIndex">The type that can be used to index on the grid.</typeparam>
    public interface IGrid<in TIndex> : IGrid
    {
        int this[TIndex index] { set; }
    }

    /// <summary>
    /// Interface for a grid of integers indexed with <see cref="ICoordinate"/>
    /// </summary>
    public interface IGrid
    {
        /// <summary>
        /// Applies the <paramref name="action"/> on each <see cref="ICoordinate"/> and its corresponding value.
        /// </summary>
        void ForEach(Action<ICoordinate, int> action);

        /// <summary>
        /// Iterates over each neighborhood patch with the given <paramref name="radius"/>
        /// and applies the <paramref name="selector"/>.
        /// </summary>
        IGrid SelectNeighborhood(int radius, Func<INeighborhood, int> selector);
    }
}