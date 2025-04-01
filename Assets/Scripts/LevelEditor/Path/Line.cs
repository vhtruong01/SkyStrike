using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Line : MonoBehaviour
        {
            [SerializeField] private RawImage dot;
            private ObjectPool<RawImage> imagePool;
            private Stack<RawImage> dots;

            public void Awake()
            {
                imagePool = new(createFunc: Create, actionOnGet: Get, actionOnRelease: Release);
                dots = new();
            }
            private RawImage Create() => Instantiate(dot, transform, false);
            private void Release(RawImage img) => img.gameObject.SetActive(false);
            private void Get(RawImage img) => img.gameObject.SetActive(true);
            public void Draw(Vector2[] positions)
            {
                for (int i = dots.Count - 1; i >= positions.Length; i--)
                    imagePool.Release(dots.Pop());
                for (int i = dots.Count; i < positions.Length; i++)
                    dots.Push(imagePool.Get());
                int index = 0;
                foreach (var d in dots)
                {
                    d.transform.position = positions[index].SetZ(d.transform.position.z);
                    index++;
                }
            }
        }
    }
}