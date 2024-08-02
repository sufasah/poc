using System.Text;
using Algorithms.Graph.ShortestPath;
using Algorithms.Test.Core.Random;
using static Algorithms.Graph.Graph;

namespace Algorithms.Test.Graph.ShortestPath;

public static class Tester
{
    public static void Test(IShortestPathFindable findable)
    {
        const short nodeCount = 10;
        for (var testCount = 0; testCount < 100; testCount++)
        {
            var nodes = new Node[nodeCount];

            for (var i = 0; i < nodeCount; i++)
                nodes[i] = new Node(i);

            for (var i = 0; i < nodeCount; i++)
                for (var j = 0; j < nodeCount; j++)
                    if (i != j)
                        nodes[i].AddEdge(nodes[j], Generator.Random.Next(1, 10));

            var from = Generator.Random.Next(0, nodeCount);
            var to = from;
            while (to == from)
                to = Generator.Random.Next(0, nodeCount);

            var actualPath = findable.FindShortestPath(nodes[from], nodes[to]);
            var allPaths = DepthFirstSearch.FindAllPaths(nodes[from], nodes[to]);

            AssertionHelper.WithMessage(
                () => Assert.Contains(
                    allPaths.GroupBy(GetPathCost).MinBy(g => g.Key),
                    path => actualPath.SequenceEqual(path)),
                () => new StringBuilder()
                    .AppendLine("Actual shortest path hasn't been found in the possible paths as shortest one.")
                    .AppendLine()
                    .AppendTestInfo(actualPath, allPaths)
                    .ToString());
        }
    }

    private static StringBuilder AppendTestInfo(this StringBuilder builder,
        IEnumerable<Node> actualPath,
        ICollection<Node[]> allPaths)
    {
        builder.AppendLine("Actual Shortest Path:")
            .Append(GetPathString(actualPath)).AppendLine($" Cost: {GetPathCost(actualPath)}")
            .AppendLine()
            .AppendLine($"All Paths Count: {allPaths.Count}")
            .AppendLine("All Paths:");

        foreach (var path in allPaths)
            builder.Append(GetPathString(path))
                .AppendLine($" Cost: {GetPathCost(path)}");

        return builder;
    }
}