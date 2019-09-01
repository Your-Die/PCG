using System.Collections.Generic;
using Chinchillada.Generation;

namespace Generation.Grid
{
    public interface INeighborhood
    {
        int Center { get; }
        
        IEnumerable<int> Horizontal();
        IEnumerable<int> Vertical();
        IEnumerable<int> Orthogonal();
        
        IEnumerable<int> EastDiagonal();
        IEnumerable<int> WestDiagonal();
        IEnumerable<int> Diagonal();
        
        IEnumerable<int> Full();
    }
}