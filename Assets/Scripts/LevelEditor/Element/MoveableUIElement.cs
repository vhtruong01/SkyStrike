using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public abstract class MoveableUIElement<T> : UIElement<T>, IBeginDragHandler, IDragHandler where T : IEditor
        {
            protected bool isDragging;
            public IScalableScreen screen { protected get; set; }

            public override void OnPointerClick(PointerEventData eventData)
            {
                if (!isDragging) Click();
                isDragging = false;
            }
            public void OnBeginDrag(PointerEventData eventDataF) => isDragging = true;
            public virtual void OnDrag(PointerEventData eventData)
            {
                Vector2 pos = eventData.pointerCurrentRaycast.worldPosition;
                if (screen.isSnapping)
                    pos = screen.RoundPosition(pos);
                transform.position = pos.SetZ(transform.position.z);
            }
            public void SetPosition(Vector2 newPos)
            {
                newPos = screen.GetPositionOnScreen(newPos);
                if (!newPos.IsAlmostEqual(transform.position))
                {
                    transform.position = newPos.SetZ(transform.position.z);
                    Refresh();
                }
            }
            protected virtual void Refresh() { }
        }
    }
}