using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class ViewportItemUI : UIElement, IDragHandler
        {
            [SerializeField] private Image icon;
            private bool isDrag;

            public override void SetData(IEditorData data)
            {
                var objectDataObserver = data as ObjectDataObserver;
                icon.sprite = objectDataObserver.metaData.data.sprite;
                icon.color = objectDataObserver.metaData.data.color;
                base.SetData(data);
            }
            private void SetPosition(Vector2 pos)
            {
                if (transform.position.x == pos.x && transform.position.y == pos.y)
                    return;
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
                isDrag = true;
                (data as ObjectDataObserver).position.SetData(transform.position);
            }
            public override void OnPointerClick(PointerEventData eventData)
            {
                if (!isDrag)
                    InvokeData();
                isDrag = false;
            }
            public override void BindData()
            {
                var objectDataObserver = data as ObjectDataObserver;
                objectDataObserver.position.Bind(SetPosition);
                objectDataObserver.scale.Bind(SetScale);
                objectDataObserver.rotation.Bind(SetRotation);
            }
            public override void UnbindData()
            {
                var objectDataObserver = data as ObjectDataObserver;
                objectDataObserver.position.Unbind(SetPosition);
                objectDataObserver.scale.Unbind(SetScale);
                objectDataObserver.rotation.Unbind(SetRotation);
            }
        }
    }
}