namespace Algorithms.String.SubstringSearch;

/// <summary>
/// Knuth-Morris-Pratt
/// </summary>
public class KMP : ISubstringSearchable
{
    public int FindSubstrIndex(string searching, string substring)
    {
        var kmpTable = new Dictionary<int, int>();
        var i = 0;
        var j = 1;
        while (j < substring.Length)
        {
            if (substring[i] == substring[j])
            {
                i++;
                j++;
                kmpTable.Add(j, i);
                continue;
            }

            if (i >= 1)
            {
                i = kmpTable.TryGetValue(i, out var matchLength) ? matchLength : 0;
                continue;
            }
            
            j++;
        }

        i = j = 0;
        while (i < searching.Length && j < substring.Length)
        {
            if (searching[i] == substring[j])
            {
                j++;
                i++;
            }
            else if (j >= 1)
                j = kmpTable.TryGetValue(j, out int matchLength) ? matchLength : 0;
            else
                i++;
        }

        if (j < substring.Length)
            return -1;
        
        return i - substring.Length;
    }
}