namespace Algorithms;
public static class Sorting
{
    public static void HeapSort(int[] arr)
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

    public static void QuickSort(int[] arr)
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

    public static void Test(Action<int[]> algorithm)
    {
        const int arrSize = 25;
        var testCount = 1000000;
        while (testCount > 0)
        {
            var arr = new int[arrSize];
            var arrFirst = new int[arrSize];

            var rand = new Random();
            for (int i = 0; i < arr.Length; i++)
                arr[i] = rand.Next(1, 1000);
            arr.CopyTo(arrFirst, 0);

            algorithm.Invoke(arr);

            var sorted = new int[25];
            arr.CopyTo(sorted, 0);
            Array.Sort(sorted);
            var isSorted = sorted.Select((item, i) => item == arr[i]).All(x => x);
            if (!isSorted)
            {
                Console.WriteLine(string.Join(',', arrFirst));
                Console.WriteLine(string.Join(',', arr));
                Console.WriteLine(string.Join(',', sorted));
                Console.WriteLine($"IsSorted: {isSorted}");
                break;
            }
            testCount--;
        }

        if (testCount == 0)
            Console.WriteLine("Passed Successfully");
    }
}