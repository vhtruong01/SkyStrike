using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class GridScreen : MonoBehaviour, IScalableScreen
        {
            private readonly static float epsilon = 1f / 3;
            private static readonly Color axisColor = new(1, 0, 0, 0.33f);
            private static readonly Color subaxisColor = new(1, 1, 1, 0.05f);
            private static readonly float minSize = 0.5f;
            private static readonly float maxSize = 1.25f;
            [SerializeField] private float thickness;
            [SerializeField] private Slider scaleSlider;
            [SerializeField] private Transform lineContainer;
            private NormalButton lockButton;
            private Vector3 originalScale;
            private float halfWidth;
            private float halfHeight;
            public bool isLocked { get; set; }
            public bool isSnapping {  get; set; }
            public float scale
            {
                get => scaleSlider.value;
                set
                {
                    scaleSlider.value = value;
                    Scale(value);
                }
            }
            public void Init()
            {
                lockButton = scaleSlider.GetComponentInChildren<NormalButton>();
                lockButton.AddListener((enable) => isLocked = enable, () => isLocked);
                if (thickness <= 0) return;
                scaleSlider.minValue = minSize;
                scaleSlider.maxValue = maxSize;
                originalScale = transform.localScale;
                scaleSlider.onValueChanged.AddListener(Scale);
                Vector2 worldSize = Controller.mainCam.ScreenToWorldPoint(new(Screen.width, Screen.height));
                halfWidth = worldSize.x;
                halfHeight = worldSize.y;
                for (int i = -(int)(halfWidth / minSize); i <= halfWidth / minSize; i++)
                    CreateLine(thickness, Screen.height / minSize, new(i, 0, 0));
                for (int i = -(int)(halfHeight / minSize); i <= halfHeight / minSize; i++)
                    CreateLine(Screen.width / minSize, thickness, new(0, i, 0));
            }
            private void CreateLine(float w, float h, Vector3 dir)
            {
                var img = new GameObject("Line").AddComponent<Image>();
                img.transform.SetParent(lineContainer, false);
                img.rectTransform.sizeDelta = new Vector2(w, h);
                img.transform.position = img.transform.position + dir;
                img.color = dir == Vector3.zero ? axisColor : subaxisColor;
            }
            public void Scale(float size)
            {
                transform.localScale = (originalScale * size).SetZ(originalScale.z);
                SetPosition(transform.position);
            }
            public Vector2 GetActualPosition(Vector2 pos)
                => (pos - new Vector2(transform.position.x, transform.position.y)) / scale;
            public Vector2 GetPositionOnScreen(Vector2 pos)
                => pos * scale + new Vector2(transform.position.x, transform.position.y);
            public void SetPosition(Vector2 newPos)
            {
                float boundX = halfWidth * (scale / minSize - 1);
                float boundY = halfHeight * (scale / minSize - 1);
                newPos.Set(Mathf.Clamp(newPos.x, -boundX, boundX),
                           Mathf.Clamp(newPos.y, -boundY, boundY));
                transform.position = newPos.SetZ(transform.position.z);
            }
            public Vector2 RoundPosition(Vector2 pos)
            {
                pos = GetActualPosition(pos);
                float deltaX = pos.x - Mathf.RoundToInt(pos.x);
                if (Mathf.Abs(deltaX) <= epsilon)
                    pos.x -= deltaX;
                float deltaY = pos.y - Mathf.RoundToInt(pos.y);
                if (Mathf.Abs(deltaY) <= epsilon)
                    pos.y -= deltaY;
                return GetPositionOnScreen(pos);
            }
        }
    }
}