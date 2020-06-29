using System.Collections.Generic;

namespace Chinchillada.Generation.Mazes
{
    public interface IGraph
    {
        IEnumerable<IGraphNode> Nodes { get; }
    }
}