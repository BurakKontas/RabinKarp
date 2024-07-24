var res = RabinKarp.Search("abaaa", "ab");
Console.WriteLine(string.Join(", ", res));

class RabinKarp
{
    static int prime = 101;

    public static List<int> Search(string text, string pattern)
    {
        var m = pattern.Length;
        var n = text.Length;
        var patternHash = CreateHash(pattern, m - 1);
        var textHash = CreateHash(text, m - 1);
        var result = new List<int>();

        for (var i = 0; i <= n - m; i++)
        {
            //if (IsOneMismatch(pattern, text, i))
            if (patternHash == textHash && CheckEqual(text, i, i + m - 1, pattern, 0, m - 1))
            {
                result.Add(i);
            }
            if (i < n - m)
            {
                textHash = RecalculateHash(text, i, i + m, textHash, m);
            }
        }
        return result;
    }

    static long CreateHash(string str, int end)
    {
        long hash = 0;
        for (var i = 0; i <= end; i++)
        {
            hash += str[i] * (long)Math.Pow(prime, i);
        }
        return hash;
    }

    static long RecalculateHash(string str, int oldIndex, int newIndex, long oldHash, int patternLen)
    {
        var newHash = oldHash - str[oldIndex];
        newHash /= prime;
        newHash += str[newIndex] * (long)Math.Pow(prime, patternLen - 1);
        return newHash;
    }

    // checks with 1 mismatch (line 82)
    static bool IsOneMismatch(string pattern, string text, int startIndex)
    {
        var mismatchCount = 0;
        for (var j = 0; j < pattern.Length; j++)
        {
            if (text[startIndex + j] == pattern[j]) continue;

            mismatchCount++;

            if (mismatchCount > 1)
            {
                return false;
            }
        }
        return true;
    }

    // checks full pattern match
    static bool CheckEqual(string str1, int start1, int end1, string str2, int start2, int end2)
    {
        if (end1 - start1 != end2 - start2)
        {
            return false;
        }
        while (start1 <= end1 && start2 <= end2)
        {
            if (str1[start1] != str2[start2])
            {
                return false;
            }
            start1++;
            start2++;
        }
        return true;
    }
}