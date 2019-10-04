using Chinchillada.Generation.Grid;

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
        IGrid Step(IGrid grid);

        int ApplyRules(INeighborhood neighborhood);
        int CountNeighbors(INeighborhood neighborhood);
    }
}