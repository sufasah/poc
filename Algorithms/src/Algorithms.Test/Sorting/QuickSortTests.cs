using Algorithms.Sorting;

namespace Algorithms.Test.Sorting;

public class QuickSortTests
{
    private readonly QuickSort<int> _sut;

    public QuickSortTests()
    {
        _sut = new QuickSort<int>();
    }

    [Fact]
    public void Sort() => Tester.Test(_sut);
}