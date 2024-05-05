using static Algorithms.Graph.Graph;

namespace Algorithms.Graph.ShortestPath;

public class Dijkstra : IShortestPathFindable
{
    public ICollection<Node> FindShortestPath(Node from, Node to)
        => Find(from, to);

    public static List<Node> Find(
        Node from,
        Node to,
        IEnumerable<Node>? graph = null)
    {
        var result = new List<Node>();
        var table = graph == null
            ? new Dictionary<Node, DjikstraTableData>
            {
                { from, new() { Cost = 0 } },
                { to, new() }
            }
            : graph.ToDictionary(node => node, node => new DjikstraTableData());
        if (graph != null)
            table[from].Cost = 0;

        var node = from;

        while (node != to)
        {
            var data = table[node];
            data.IsDiscovered = true;
            var edges = node.Edges.Select(edge =>
                {
                    var otherNode = edge.GetOther(node)!;
                    DjikstraTableData? otherData;

                    if (graph != null)
                        otherData = table[otherNode];
                    else if (!table.TryGetValue(otherNode, out otherData))
                        table[otherNode] = otherData = new();

                    return (Edge: edge, To: otherNode, Data: otherData);
                })
                .ToArray();

            foreach (var item in edges)
            {
                var otherCost = data.Cost + item.Edge.Cost;
                if (otherCost < item.Data.Cost)
                {
                    item.Data.Parent = node;
                    item.Data.Cost = otherCost;
                }
            }

            node = edges.Where(item => !item.Data.IsDiscovered)
                .MinBy(item => item.Data.Cost)
                .To;
        }

        while (node != from)
        {
            result.Add(node!);
            node = table[node!].Parent;
        }

        result.Add(node);
        result.Reverse();

        return result;
    }

    private sealed class DjikstraTableData
    {
        public Node? Parent { get; set; } = null;
        public int Cost { get; set; } = int.MaxValue;
        public bool IsDiscovered { get; set; } = false;
    }
}