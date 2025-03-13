using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class DraggableElement : MonoBehaviour, IDragHandler, IBeginDragHandler
        {
            private static Camera _mainCam;
            private static Camera mainCam
            {
                get
                {
                    if (_mainCam == null)
                        _mainCam = Camera.main;
                    return _mainCam;
                }
            }
            private Vector2 delta;

            public void OnDrag(PointerEventData eventData)
            {
                Vector3 newPos = mainCam.ScreenToWorldPoint(new(
                    Mathf.Clamp(eventData.position.x - delta.x, 0, Screen.width),
                    Mathf.Clamp(eventData.position.y - delta.y, 0, Screen.height),
                    0));
                newPos.z = transform.position.z;
                transform.position = newPos;
            }
            public void OnBeginDrag(PointerEventData eventData)
            {
                Vector2 curPos = mainCam.WorldToScreenPoint(transform.position);
                delta = eventData.position - curPos;
            }
        }
    }
}