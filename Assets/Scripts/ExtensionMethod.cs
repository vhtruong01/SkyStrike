using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SkyStrike
{
    public class ReadOnlyAttribute : PropertyAttribute { }
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property, label, true);
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
    public static class ExtensionMethod
    {
        public static int NULL_ID { get; } = -1;
        public static readonly Color[] colors = new Color[] {
            new(1, 1, 0.5f),
            new(1, 0.5f, 1),
            new(0.5f, 1, 1),
            new(1, 0.5f, 0.5f),
            new(0.5f, 0.5f, 1),
            new(0.5f, 1, 0.5f),
            new(1, 1, 0.75f),
            new(1, 0.75f, 1),
            new(0.75f, 1, 1),
            new(1, 0.75f, 0.75f),
            new(0.75f, 0.75f, 1),
            new(0.75f, 1, 0.75f),
            new(0.75f, 0.75f, 0.5f),
            new(0.75f, 0.5f, 0.75f),
            new(0.5f, 0.75f, 0.75f),
            new(0.75f, 0.5f, 0.5f),
            new(0.5f, 0.5f, 0.75f),
            new(0.5f, 0.75f, 0.5f),
            new(1, 0.75f, 0.5f),
            new(1, 0.5f, 0.75f),
            new(0.75f, 0.5f, 1),
            new(0.75f, 1, 0.5f),
            new(0.5f, 0.75f, 1),
            new(0.5f, 1, 0.75f),
        };
        // List
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
            if (rightIndex >= list.Count)
            {
                rightIndex = leftIndex;
                leftIndex = 0;
            }
            (list[leftIndex], list[rightIndex]) = (list[rightIndex], list[leftIndex]);
        }
        // Vector
        public static bool IsAlmostEqual(this Vector2 v1, Vector2 v2)
            => Mathf.Abs(v1.x - v2.x) <= 0.00001f && Mathf.Abs(v1.y - v2.y) <= 0.00001f;
        public static Vector2 ToVector2(this Vector3 v) => new(v.x, v.y);
        public static Vector3 SetZ(this Vector2 v, float z) => new(v.x, v.y, z);
        public static Vector3 SetZ(this Vector3 v, float z) => new(v.x, v.y, z);
        // Color
        public static Color ChangeAlpha(this Color color, float alpha) => new(color.r, color.g, color.b, alpha);
        public static Color RandomColor(this GameObject obj) => colors[Random.Range(0, colors.Length)];
        // ScriptableObject
        public static void print(this ScriptableObject obj, object msg) => Debug.Log(msg);
    }
}