namespace Algorithms.Array;

public interface IArrayProcessable<in T, out TResult>
{
    TResult Process(T[] arr);
}