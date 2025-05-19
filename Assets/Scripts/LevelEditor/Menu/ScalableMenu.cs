using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike.Editor
{
    public abstract class ScalableMenu : EventNotifyMenu, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] protected GridScreen screen;
        [SerializeField] protected NormalButton snapBtn;
        private Vector3 curPos;
        private Vector3 startPos;
        protected bool isDragging;

        public virtual void Start()
           => snapBtn.AddListener((b) => screen.isSnapping = b, () => screen.isSnapping);
        public void OnBeginDrag(PointerEventData eventData)
        {
            isDragging = true;
            curPos = screen.transform.position;
            startPos = eventData.pointerCurrentRaycast.worldPosition;
        }
        public void OnDrag(PointerEventData eventData)
        {
            if (!screen.isLocked && eventData.position.x > 0 && eventData.position.y > 0 && eventData.position.x < Screen.width && eventData.position.y < Screen.height)
                screen.SetPosition(curPos + (eventData.pointerCurrentRaycast.worldPosition - startPos));
        }
        public void OnEndDrag(PointerEventData eventData) => isDragging = false;
    }
}