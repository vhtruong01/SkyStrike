using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class MovingBackground : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Image bg1;
        private Image bg2;
        private float height;
        private Vector3 delta;
        private float maxDistance;

        public void Awake()
        {
            height = bg1.GetComponent<RectTransform>().sizeDelta.y;
            bg1.transform.localPosition = new();
            if (bg2 == null)
                bg2 = Instantiate(bg1, transform, false);
            bg2.transform.localPosition = new(0, -height, 0);
            maxDistance = height / 2 - Screen.height;
        }
        public void Update()
        {
            delta = speed * Time.deltaTime * Vector3.down;
            bg1.transform.Translate(delta);
            bg2.transform.Translate(delta);
            if (-bg1.transform.localPosition.y > maxDistance)
            {
                Vector3 bg1Pos = bg1.transform.localPosition;
                bg1Pos.y += height;
                bg2.transform.localPosition = bg1Pos;
                (bg1, bg2) = (bg2, bg1);
            }
        }
        public void SetData(Sprite sprite)
        {
            bg1.sprite = sprite;
            bg2.sprite = sprite;
        }
    }
}