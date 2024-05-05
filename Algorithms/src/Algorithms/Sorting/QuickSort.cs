using System.Numerics;

namespace Algorithms.Sorting;

public class QuickSort<T> : ISortable<T>
    where T : IComparisonOperators<T, T, bool>
{
    public void Sort(T[] arr)
    {
        var operations = new Stack<(int from, int to)>();
        operations.Push((0, arr.Length - 1));

        while (operations.Count > 0)
        {
            var (from, to) = operations.Pop();
            var i = from;
            var j = to + 1;

            while (true)
            {
                do i++;
                while (i < j && arr[i] <= arr[from]);

                do j--;
                while (arr[j] >= arr[from] && i < j);

                if (i >= j)
                    break;

                (arr[i], arr[j]) = (arr[j], arr[i]);
            }

            (arr[i - 1], arr[from]) = (arr[from], arr[i - 1]);

            if (i < to)
                operations.Push((i, to));
            if (i - 2 > from)
                operations.Push((from, i - 2));
        }
    }
}