using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class ScalableMenu : EventNotifyMenu, IDragHandler, IBeginDragHandler, IEndDragHandler
        {
            [SerializeField] protected GridScreen screen;
            [SerializeField] protected NormalButton snapBtn;
            private Vector3 curPos;
            private Vector3 startPos;
            protected bool isDrag;

            public override void Init()
            {
                base.Init();
                screen.Init();
            }
            public virtual void OnBeginDrag(PointerEventData eventData)
            {
                isDrag = true;
                curPos = screen.transform.position;
                startPos = eventData.pointerCurrentRaycast.worldPosition;
            }
            public virtual void OnDrag(PointerEventData eventData)
            {
                if (eventData.position.x > 0 && eventData.position.y > 0 && eventData.position.x < Screen.width && eventData.position.y < Screen.height)
                    screen.SetPosition(curPos + (eventData.pointerCurrentRaycast.worldPosition - startPos));
            }
            public void OnEndDrag(PointerEventData eventData) => isDrag = false;
        }
    }
}