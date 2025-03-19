using System.Collections.Generic;

public static class ExtensionMethod
{
    public static void Swap<T>(this List<T> list, int leftIndex, int rightIndex)
    {
        if (leftIndex == rightIndex) return;
        if (leftIndex > rightIndex)
            (leftIndex, rightIndex) = (rightIndex, leftIndex);
        if (leftIndex < 0)
        {
            leftIndex = rightIndex;
            rightIndex = list.Count - 1;
        }
        if (rightIndex >= list.Count - 1)
        {
            rightIndex = leftIndex;
            leftIndex = 0;
        }
        (list[leftIndex], list[rightIndex]) = (list[leftIndex], list[rightIndex]);
    }
}