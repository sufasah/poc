using static Algorithms.Graph.Graph;

namespace Algorithms.Graph.ShortestPath;

public static class DepthFirstSearch
{
    public static List<Node[]> FindAllPaths(Node from, Node to)
    {
        var result = new List<Node[]>();
        var stack = new Stack<(Node node, IEnumerator<Edge> enumerator)>();
        var minCost = int.MaxValue;
        var cost = 0;
        var (node, enumerator) = (from, from.Edges.GetEnumerator());

        while (node != null && enumerator != null)
        {
            var found = enumerator.MoveNext();
            if (!found && stack.TryPop(out var item))
            {
                (node, enumerator) = item;
                cost -= item.enumerator.Current.Cost;
                continue;
            }
            else if (!found)
            {
                (node, enumerator) = (null, null);
                continue;
            }

            var edge = enumerator.Current;
            var otherNode = edge.GetOther(node)!;
            var nextCost = cost + edge.Cost;

            if (stack.Any(item => item.node == otherNode))
                continue;

            if (otherNode != to)
            {
                cost = nextCost;
                stack.Push((node, enumerator));
                (node, enumerator) = (otherNode, otherNode.Edges.GetEnumerator());
                continue;
            }

            if (minCost < nextCost)
                continue;

            if (minCost > nextCost)
            {
                minCost = nextCost;
                result.Clear();
            }

            result.Add(stack
                .Select(item => item.node)
                .Reverse()
                .Append(node)
                .Append(otherNode)
                .ToArray());
        }

        return result;
    }
}