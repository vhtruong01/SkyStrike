using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Grill : MonoBehaviour
        {
            private static readonly Color axisColor = new(1, 0, 0, 0.33f);
            private static readonly Color subaxisColor = new(1, 1, 1, 0.05f);
            [SerializeField] private float thickness;

            public void Start()
            {
                if (thickness <= 0) return;
                transform.position = new(0, 0, transform.position.z);
                Camera cam = Camera.main;
                Vector2 worldSize = cam.ScreenToWorldPoint(new(Screen.width, Screen.height));
                //print(Display.main.systemWidth + " " + Screen.width);
                float screenWidth = worldSize.x;
                float screenHeight = worldSize.y;
                for (int i = -(int)screenWidth; i <= screenWidth; i++)
                    CreateLine(thickness, Screen.height, new(i, 0, 0));
                for (int i = -(int)screenHeight; i <= screenHeight; i++)
                    CreateLine(Screen.width, thickness, new(0, i, 0));
            }
            private void CreateLine(float w, float h, Vector3 dir)
            {
                var img = new GameObject("Line").AddComponent<Image>();
                img.transform.SetParent(transform, false);
                img.name = "Line";
                img.rectTransform.sizeDelta = new Vector2(w, h);
                img.transform.position = img.transform.position + dir;
                img.color = dir == Vector3.zero ? axisColor : subaxisColor;
            }
        }

    }
}