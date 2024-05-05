using Algorithms.String.SubstringSearch;

namespace Algorithms.Test.String.SubstringSearch;

public class KMPTests
{
    private readonly KMP _sut;

    public KMPTests()
    {
        _sut = new KMP();
    }

    [Fact]
    public void FindSubstrIndex() => Tester.Test(_sut);
}