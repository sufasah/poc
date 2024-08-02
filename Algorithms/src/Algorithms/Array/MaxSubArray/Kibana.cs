using System.Numerics;

namespace Algorithms.Array.MaxSubArray;

public class Kibana<T> : IArrayProcessable<T, T>
    where T: INumber<T>, IMinMaxValue<T>
{
    public T Process(T[] arr)
    {
        var result = T.MinValue;
        T sum = T.Zero;
        var i = 0;
        while (i < arr.Length && arr[i] <= T.Zero)
            result = T.Max(arr[i++], result);

        while (i < arr.Length)
        {
            while (arr[i] >= T.Zero)
            {
                sum += arr[i++];
                if (i >= arr.Length) break;
            }

            result = T.Max(result, sum);
            if (i >= arr.Length) break;
            
            while (arr[i] <= T.Zero)
            {
                sum += arr[i++];
                if (i >= arr.Length) break;
            }
            
            if (sum < T.Zero)
                sum = T.Zero;
        }

        return result;
    }
}