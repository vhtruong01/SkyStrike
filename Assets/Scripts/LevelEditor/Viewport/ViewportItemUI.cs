using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class ViewportItemUI : UIElement, IPointerDownHandler, IDragHandler
        {
            [SerializeField] private Image icon;

            public override void SetData(IEditorData data)
            {
                this.data = data;
                var objectDataObserver = this.data as ObjectDataObserver;
                objectDataObserver.position.Bind(SetPosition);
                objectDataObserver.scale.Bind(SetScale);
                objectDataObserver.rotation.Bind(SetRotation);
                icon.sprite = objectDataObserver.metaData.data.sprite;
                icon.color = objectDataObserver.metaData.data.color;
            }
            private void SetPosition(Vector2 pos)
            {
                transform.position = new(pos.x, pos.y, transform.position.z);
            }
            private void SetScale(Vector2 scale)
            {
                transform.localScale = new(scale.x, scale.y, transform.localScale.z);
            }
            private void SetRotation(float rotationZ)
            {
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, rotationZ);
            }
            public void OnDrag(PointerEventData eventData)
            {
                (data as ObjectDataObserver).position.OnlySetData(transform.position);
            }
            public void OnPointerDown(PointerEventData eventData) => InvokeData();
            public override void OnPointerClick(PointerEventData eventData) { }
            public override void RemoveData()
            {
                var objectDataObserver = data as ObjectDataObserver;
                objectDataObserver.position.Unbind(SetPosition);
                objectDataObserver.scale.Unbind(SetScale);
                objectDataObserver.rotation.Unbind(SetRotation);
                base.RemoveData();
            }
        }
    }
}