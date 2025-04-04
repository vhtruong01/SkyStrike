using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ScalableMenu : Menu, IDragHandler, IBeginDragHandler, IEndDragHandler
        {
            [SerializeField] protected GridScreen screen;
            private Vector3 curPos;
            private Vector3 startPos;
            protected bool isDrag;

            public virtual void OnBeginDrag(PointerEventData eventData)
            {
                isDrag = true;
                curPos = screen.transform.position;
                startPos = eventData.pointerCurrentRaycast.worldPosition;
            }
            public virtual void OnDrag(PointerEventData eventData)
                => screen.SetPosition(curPos + (eventData.pointerCurrentRaycast.worldPosition - startPos));
            public void OnEndDrag(PointerEventData eventData) => isDrag = false;
        }
    }
}