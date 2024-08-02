using System.Text;
using Algorithms.Array.MaxSubArray;
using Algorithms.Test.Core.Random;

namespace Algorithms.Test.Array.MaxSubArray;

public class KibanaTests
{
    private Kibana<int> _sut;

    public KibanaTests()
    {
        _sut = new Kibana<int>();
    }

    [Theory]
    [InlineData(100)]
    [InlineData(10000)]
    [InlineData(100000)]
    public void Process_RandomNumberArray_ResultsSameAsBruteForce(int arraySize)
    {
        var array = Enumerable.Range(0, arraySize)
            .Select(_ => Generator.Random.Next(-5000, 5000))
            .ToArray();
        var expected = new BruteForce<int>().Process(array); 
        
        var actual = _sut.Process(array);

        AssertionHelper.WithMessage(
            () => Assert.Equal(expected, actual),
            () => new StringBuilder()
                .AppendLine("Array: ").AppendJoin(',', array)
                .ToString());
    }
}