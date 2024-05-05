using System.Numerics;

namespace Algorithms.Sorting;

public class HeapSort<T> : ISortable<T>
    where T : IComparisonOperators<T, T, bool>
{
    /// <summary>
    /// Sorts from small to the large (makes max heap).
    /// </summary>
    public void Sort(T[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
        {
            var item = i + 1;
            var parent = item / 2;

            while (parent > 0)
            {
                if (arr[parent - 1] < arr[item - 1])
                    (arr[parent - 1], arr[item - 1]) = (arr[item - 1], arr[parent - 1]);

                item = parent;
                parent = item / 2;
            }
        }

        for (int i = arr.Length - 1; i >= 0; i--)
        {

            (arr[i], arr[0]) = (arr[0], arr[i]);
            var item = 1;
            var child = 2;

            while (child - 1 < i)
            {
                if (arr[child - 1] < arr[child] && child < i)
                    child++;

                if (arr[item - 1] >= arr[child - 1])
                    break;

                (arr[item - 1], arr[child - 1]) = (arr[child - 1], arr[item - 1]);
                item = child;
                child = item * 2;
            }
        }
    }
}