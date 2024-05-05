namespace Algorithms.Graph;

public static partial class Graph
{
    public static string GetPathString(IEnumerable<Node> path)
        => string.Join(" -> ", path.Select(node => node.Name));

    public static int GetPathCost(IEnumerable<Node> path)
    {
        var enumerator = path.GetEnumerator();
        if (!enumerator.MoveNext())
            return 0;
        
        var from = enumerator.Current;

        if (!enumerator.MoveNext())
            return 0;

        var result = 0;
        Node to;

        do
        {
            to = enumerator.Current;
            result += from.GetEdge(to).Cost;
            from = to;
        } while (enumerator.MoveNext());

        return result;
    }
}