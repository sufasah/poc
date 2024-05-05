namespace Algorithms.Graph;

public partial class Graph
{
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
}