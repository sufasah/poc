using System.Text;
using Algorithms.String.SubstringSearch;

namespace Algorithms.Test.String.SubstringSearch;

public class Tester
{
    public static void Test(ISubstringSearchable searchable)
    {
        const int searchStrSize = 60;
        var rand = new Random();
        string searching, substring;
        for (var searchStrCount = 0; searchStrCount < 2000; searchStrCount++)
        {
            searching = string.Join(string.Empty,
                Enumerable.Range(0, rand.Next(searchStrSize + 1))
                    .Select(_ => (char)('a' + rand.Next(0, 3))));
            for (var testCount = 0; testCount < 2000; testCount++)
            {
                var from = rand.Next(0, searching.Length);
                var to = rand.Next(from, searching.Length + 1);
                substring = searching[from..to];

                var actualIndex = searchable.FindSubstrIndex(searching, substring);
                var expectedIndex = searching.IndexOf(substring);

                AssertionHepler.WithMessage(
                    () => Assert.Equal(expectedIndex, actualIndex),
                    () => new StringBuilder()
                        .Append("Search String: ").AppendLine(searching)
                        .AppendLine()
                        .Append("Substring: ").AppendLine(substring)
                        .AppendLine()
                        .AppendLine($"Actual Index: {actualIndex}")
                        .AppendLine($"Expected Index: {expectedIndex}")
                        .ToString());
            }
        }
    }
}