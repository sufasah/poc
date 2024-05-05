using static Algorithms.Graph.Graph;

namespace Algorithms.Graph.ShortestPath;

public interface IShortestPathFindable
{
    ICollection<Node> FindShortestPath(Node from, Node to);
}