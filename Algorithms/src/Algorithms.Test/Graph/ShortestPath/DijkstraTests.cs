using Algorithms.Graph.ShortestPath;

namespace Algorithms.Test.Graph.ShortestPath;

public class DijkstraTests
{
    private readonly Dijkstra _sut;

    public DijkstraTests()
    {
        _sut = new Dijkstra();
    }

    [Fact]
    public void FindShortestPath() => Tester.Test(_sut);
}