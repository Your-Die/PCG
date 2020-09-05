using System.Collections.Generic;

namespace Chinchillada.Generation.Mazes
{
    public interface IGraphNode
    {
        IEnumerable<IGraphNode> Connections { get; }
    }
}