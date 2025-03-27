using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class DraggableElement : MonoBehaviour, IDragHandler, IBeginDragHandler
        {
            [SerializeField] protected RectTransform rectTransform;
            protected Vector2 delta;

            public void Awake()
            {
                if (rectTransform == null)
                    rectTransform = GetComponent<RectTransform>();
            }
            protected virtual void SetPosition(Vector3 pos) => rectTransform.position = pos;
            public void OnDrag(PointerEventData eventData)
            {
                Vector3 pos = Controller.mainCam.ScreenToWorldPoint(new(
                    Mathf.Clamp(eventData.position.x - delta.x, 0, Screen.width),
                    Mathf.Clamp(eventData.position.y - delta.y, 0, Screen.height),
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
}