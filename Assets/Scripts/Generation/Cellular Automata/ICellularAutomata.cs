namespace Chinchillada.Generation.CellularAutomata
{
    /// <summary>
    /// Interface for classes that perform cellular automata operations.
    /// </summary>
    public interface ICellularAutomata
    {
        /// <summary>
        /// Perform a step of cellular automata on the <paramref name="grid"/>.
        /// </summary>
        /// <returns>The result of the step.</returns>
        Grid2D Step(Grid2D grid);


        /// <summary>
        /// Perform a step of cellular automata on the <paramref name="grid"/>.
        /// </summary>
        /// <param name="grid">The grid we are performing a step on.</param>
        /// <param name="buffer">Used to store intermediate data.</param>
        /// <returns>The result of the step.</returns>
        void Step(ref Grid2D grid, Grid2D buffer);
    }
}