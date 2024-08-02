using System.Numerics;

namespace Algorithms.Array.MaxSubArray;

public class BruteForce<T> : IArrayProcessable<T, T>
    where T : INumber<T>, IMinMaxValue<T>
{
    public T Process(T[] arr)
    {
        var result = T.MinValue;

        for (int i = 0; i < arr.Length; i++)
        {
            var sum = T.Zero;
            for (int j = i; j < arr.Length; j++)
            {
                sum += arr[j];
                result = T.Max(sum, result);
            }
        }

        return result;
    }
}