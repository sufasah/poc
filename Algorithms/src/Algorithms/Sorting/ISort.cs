namespace Algorithms.Sorting;

public interface ISortable<in T>
{
    void Sort(T[] arr);
}