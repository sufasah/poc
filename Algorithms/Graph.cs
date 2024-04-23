namespace Algorithms;

public static class Graph
{
    public static List<Node> Dijkstra(Node from, Node to, Node[]? graph = null)
    {
        var result = new List<Node>();
        var table = graph == null
            ? new Dictionary<Node, DjikstraTableData>
            {
                    { from, new DjikstraTableData { Cost = 0 } },
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

    public static List<Node[]> DFS(Node from, Node to)
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

    public static void Test()
    {
        var random = new Random();
        const short nodeCount = 10;
        var testCount = 100;

        while (testCount > 0)
        {
            var nodes = new Node[nodeCount];

            for (var i = 0; i < nodeCount; i++)
                nodes[i] = new Node(i);

            for (var i = 0; i < nodeCount; i++)
                for (var j = 0; j < nodeCount; j++)
                    if (i != j)
                        nodes[i].AddEdge(nodes[j], random.Next(1, 10));

            var from = random.Next(0, nodeCount);
            var to = from;
            while (to == from)
                to = random.Next(0, nodeCount);

            var path = Dijkstra(nodes[from], nodes[to], nodes);
            var pathStr = string.Join(" -> ", path.Select(node => node.Name));

            var dfsPaths = DFS(nodes[from], nodes[to]);
            var containsDjikstraPath = dfsPaths.Exists(dfsPath => path.SequenceEqual(dfsPath));
            if (!containsDjikstraPath)
            {
                Console.WriteLine("--- Result ---");
                Console.WriteLine($"Djikstra: {pathStr}");
                Console.WriteLine($"DFS: ContainsDjikstraPath={containsDjikstraPath}, PathCount={dfsPaths.Count}");
                Console.WriteLine($"DFS paths:");
                foreach (var dfsPath in dfsPaths)
                {
                    var pstr = string.Join(" -> ", dfsPath.Select(node => node.Name));
                    var cost = dfsPath.SkipLast(1).Select((node, i) => node.GetEdge(dfsPath[i + 1])).Sum(edge => edge.Cost);
                    Console.WriteLine($"{pstr}  Cost: {cost}");
                }
                break;
            }
            testCount--;
        }

        if (testCount == 0)
            Console.WriteLine("Passed Successfully");
    }

    public class Node(int index)
    {
        private readonly HashSet<Edge> _edges = [];

        public int Index { get; private set; } = index;
        public string Name => $"N{Index + 1}";
        public IEnumerable<Edge> Edges => _edges;

        public Edge GetEdge(Node to)
        {
            return _edges.First(edge => edge.GetOther(this) == to);
        }

        public bool AddEdge(Node to, int cost)
        {
            return _edges.Add(new Edge(cost, this, to));
        }

        public bool RemoveEdge(Node to)
        {
            return RemoveEdge(_edges.First(edge => edge.Right == to));
        }

        public bool RemoveEdge(Edge edge)
        {
            return _edges.Remove(edge);
        }

        public override string ToString() => Name;
    }

    public class Edge(int cost, Node left, Node right)
    {
        public int Cost { get; private set; } = cost;
        public Node Left { get; private set; } = left;
        public Node Right { get; private set; } = right;

        public Node? GetOther(Node node)
        {
            if (Right == node)
                return Left;
            if (Left == node)
                return Right;
            return null;
        }

        public void Delete()
        {
            Left.RemoveEdge(this);
            Right.RemoveEdge(this);
        }

        public override bool Equals(object? obj)
        {
            return obj is Edge other && Left == other.Left && Right == other.Right;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Left, Right, Cost);
        }

        public override string ToString() => $"{Left} => {Right}";
    }

    public class DjikstraTableData
    {
        public Node? Parent { get; set; } = null;
        public int Cost { get; set; } = int.MaxValue;
        public bool IsDiscovered { get; set; } = false;
    }
}