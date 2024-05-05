
using System.Text;
using Algorithms.Sorting;

namespace Algorithms.Test.Sorting;

public static class Tester
{
    public static void Test(ISortable<int> sortable)
    {
        const int arrSize = 25;

        for (var testCount = 0; testCount < 1000000; testCount++)
        {
            var arr = new int[arrSize];
            var arrFirst = new int[arrSize];

            var rand = new Random();
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next(1, 1000);
            arr.CopyTo(arrFirst, 0);

            sortable.Sort(arr);

            var sorted = new int[25];
            arr.CopyTo(sorted, 0);
            Array.Sort(sorted);

            AssertionHepler.WithMessage(
                () => Assert.Equal(sorted, arr),
                () => new StringBuilder()
                    .AppendLine("First Array:")
                    .AppendJoin(',', arrFirst).AppendLine()
                    .AppendLine()
                    .AppendLine("Actual Sorted Array:")
                    .AppendJoin(',', arr).AppendLine()
                    .AppendLine()
                    .AppendLine("Expected Sorted Array:")
                    .AppendJoin(',', sorted)
                    .ToString());

        }
    }
}