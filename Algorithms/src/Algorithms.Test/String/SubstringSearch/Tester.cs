using System.Text;
using Algorithms.String.SubstringSearch;
using Algorithms.Test.Core.Random;

namespace Algorithms.Test.String.SubstringSearch;

public class Tester
{
    public static void Test(ISubstringSearchable searchable)
    {
        const int searchStrSize = 60;
        string searching, substring;
        for (var searchStrCount = 0; searchStrCount < 2000; searchStrCount++)
        {
            searching = string.Join(string.Empty,
                Enumerable.Range(0, Generator.Random.Next(searchStrSize + 1))
                    .Select(_ => (char)('a' + Generator.Random.Next(0, 3))));
            for (var testCount = 0; testCount < 2000; testCount++)
            {
                var from = Generator.Random.Next(0, searching.Length);
                var to = Generator.Random.Next(from, searching.Length + 1);
                substring = searching[from..to];

                var actualIndex = searchable.FindSubstrIndex(searching, substring);
                var expectedIndex = searching.IndexOf(substring);

                AssertionHelper.WithMessage(
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