using UnityEngine;
using System.Collections.Generic;

public static class ExtensionMethod
{
    //List
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
    //Float
    public static float Round(this float value, float epsilon)
    {
        if (Mathf.Abs(value - Mathf.RoundToInt(value)) <= epsilon)
            value = Mathf.RoundToInt(value);
        return value;
    }
    //Vector
    public static Vector2 ToVector2(this Vector3 v) => new(v.x, v.y);
    public static Vector3 SetZ(this Vector2 v, float z) => new(v.x, v.y, z);
    public static Vector3 SetZ(this Vector3 v, float z) => new(v.x, v.y, z);
}