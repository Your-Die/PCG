using System.Collections.Generic;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// Represents a neighborhood on a <see cref="IGrid"/>.
    /// </summary>
    public interface INeighborhood
    {
        int Center { get; }
        
        IEnumerable<int> Orthogonal();
        
        IEnumerable<int> Diagonal();
        
        IEnumerable<int> Full();
    }
}