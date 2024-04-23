using Algorithms;

static void RunThreads(params ThreadStart[] operations)
{
    var threads = operations.Select(operation => new Thread(operation)).ToArray();
    foreach (var thread in threads)
        thread.Start();
    foreach (var thread in threads)
        thread.Join();
}

RunThreads(
    () => Sorting.Test(Sorting.QuickSort),
    () => Sorting.Test(Sorting.HeapSort),
    () => Graph.Test());