using System.Collections.Generic;

namespace Chinchillada.Generation.Grid
{
    /// <summary>
    /// Represents a neighborhood on a <see cref="IGrid"/>.
    /// </summary>
    public interface INeighborhood
    {
        ICoordinate Center { get; }
        
        /// <summary>
        /// The value at the center of this <see cref="INeighborhood"/>.
        /// </summary>
        int CenterValue { get; }
        
        /// <summary>
        /// The Orthogonal neighborhood around the <see cref="CenterValue"/>.
        /// </summary>
        IEnumerable<int> Orthogonal();
        
        /// <summary>
        /// The diagonal neighborhood around the <see cref="CenterValue"/>.
        /// </summary>
        IEnumerable<int> Diagonal();
        
        /// <summary>
        /// The full neighborhood around the <see cref="CenterValue"/>.
        /// </summary>
        IEnumerable<int> Full();
    }
}