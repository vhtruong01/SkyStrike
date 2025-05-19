using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

namespace SkyStrike
{
    // Unity inspector
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
    // In/Out
    public static class IO
    {
        private static readonly string path = "Assets/Resources/Levels";
        public static void WriteToBinaryFile<T>(string fileName, T objectToWrite)
        {
            string dataPath = Path.Combine(Application.persistentDataPath, fileName);
            using Stream stream = File.Open(dataPath, FileMode.OpenOrCreate);
            new BinaryFormatter().Serialize(stream, objectToWrite);
        }
        public static T ReadFromBinaryFile<T>(string fileName)
        {
            try
            {
                string dataPath = Path.Combine(Application.persistentDataPath, fileName);
                using Stream stream = File.Open(dataPath, FileMode.Open);
                return (T)new BinaryFormatter().Deserialize(stream);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
                return default;
            }
        }
        public static bool SaveLevel(object obj, string fileName)
        {
            try
            {
                MemoryStream stream = new();
                new BinaryFormatter().Serialize(stream, obj);
                var bytes = stream.ToArray();
                string filePath = GetDataPath(fileName);
                string directoryPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
                File.WriteAllBytes(filePath, bytes);
                AssetDatabase.Refresh();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }
        public static bool RenameLevel(string oldName, string newName)
        {
            string directoryPath = Path.GetDirectoryName(GetDataPath(oldName));
            if (Directory.Exists(directoryPath))
            {
                string oldPath = GetDataPath(oldName);
                File.Move(oldPath, GetDataPath(newName));
                File.Delete(oldPath + ".meta");
                AssetDatabase.Refresh();
                return true;
            }
            else
            {
                Debug.LogWarning("File not exist!");
                return false;
            }
        }
        public static T LoadLevel<T>(string fileName) where T : class
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    return null;
                var bytes = Resources.Load<TextAsset>("Levels/" + fileName).bytes;
                using MemoryStream stream = new(bytes);
                return (T)new BinaryFormatter().Deserialize(stream);
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
                return null;
            }
        }
        public static Dictionary<string, T> LoadAllLevel<T>() where T : class
        {
            try
            {
                var list = Resources.LoadAll<TextAsset>("Levels");
                Dictionary<string, T> result = new();
                for (int i = 0; i < list.Length; i++)
                {
                    using MemoryStream stream = new(list[i].bytes);
                    T temp = (T)new BinaryFormatter().Deserialize(stream);
                    if (temp != null)
                        result.Add(list[i].name, temp);
                }
                return result;
            }
            catch (Exception e)
            {
                Debug.LogWarning(e.Message);
                return null;
            }
        }
        private static string GetDataPath(string fileName)
            => $"{path}/{fileName}.txt";
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
        public static Color RandomColor(this GameObject obj) => colors[UnityEngine.Random.Range(0, colors.Length)];
        // ScriptableObject
        public static void print(this ScriptableObject obj, object msg) => Debug.Log(msg);
    }
}