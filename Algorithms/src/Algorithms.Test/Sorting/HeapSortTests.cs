using Algorithms.Sorting;

namespace Algorithms.Test.Sorting;

public class HeapSortTests
{
    private readonly HeapSort<int> _sut;

    public HeapSortTests()
    {
        _sut = new HeapSort<int>();
    }

    [Fact]
    public void Sort() => Tester.Test(_sut);
}