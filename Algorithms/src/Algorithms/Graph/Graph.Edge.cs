namespace Algorithms.Graph;

public partial class Graph
{
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
}