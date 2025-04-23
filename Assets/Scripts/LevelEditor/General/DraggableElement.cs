using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike.Editor
{
    public class DraggableElement : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [SerializeField] protected RectTransform rectTransform;
        protected Vector2 delta;
        private float halfW;
        protected float halfH;

        public void Awake()
        {
            if (rectTransform == null)
                rectTransform = GetComponent<RectTransform>();
            halfW = rectTransform.sizeDelta.x / 2;
            halfH = rectTransform.sizeDelta.y / 2;
        }
        protected virtual void SetPosition(Vector3 pos) => rectTransform.position = pos;
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pos = Controller.mainCam.ScreenToWorldPoint(new(
                Mathf.Clamp(eventData.position.x - delta.x, halfW, Screen.width - halfW),
                Mathf.Clamp(eventData.position.y - delta.y, halfH, Screen.height - halfH),
                0));
            pos.z = rectTransform.position.z;
            SetPosition(pos);
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Vector2 curPos = Controller.mainCam.WorldToScreenPoint(rectTransform.position);
            delta = eventData.position - curPos;
        }
    }
}