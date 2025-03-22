using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class ViewportItemUI : UIElement<ObjectDataObserver>, IDragHandler
        {
            [SerializeField] private Image icon;
            private bool isDrag;

            public override void SetData(ObjectDataObserver data)
            {
                base.SetData(data);
                icon.sprite = data.metaData.data.sprite;
                icon.color = data.metaData.data.color;
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
                data.position.SetData(transform.position);
            }
            public override void OnPointerClick(PointerEventData eventData)
            {
                if (!isDrag)
                    InvokeData();
                isDrag = false;
            }
            public override void BindData()
            {
                data.position.Bind(SetPosition);
                data.scale.Bind(SetScale);
                data.rotation.Bind(SetRotation);
            }
            public override void UnbindData()
            {
                data.position.Unbind(SetPosition);
                data.scale.Unbind(SetScale);
                data.rotation.Unbind(SetRotation);
            }
        }
    }
}