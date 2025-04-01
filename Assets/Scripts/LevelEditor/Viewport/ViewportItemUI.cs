using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ViewportItemUI : UIElement<ObjectDataObserver>
        {
            [SerializeField] private Image icon;
            [SerializeField] private Image refIcon;

            public override void SetData(ObjectDataObserver data)
            {
                base.SetData(data);
                icon.sprite = data.metaData.data.sprite;
                icon.color = data.metaData.data.color;
                SetRefObject(data.refData);
            }
            public void SetRefObject(ObjectDataObserver refData)
            {
                if (refData == null) refIcon.color = new();
                else
                {
                    refIcon.color = refData.metaData.data.color;
                    refIcon.sprite = refData.metaData.data.sprite;
                }
            }
            private void SetPosition(Vector2 pos)
            {
                if (transform.position.x == pos.x && transform.position.y == pos.y)
                    return;
                transform.position = new(pos.x, pos.y, transform.position.z);
            }
            public override void OnDrag(PointerEventData eventData)
            {
                base.OnDrag(eventData);
                data.SetPosition(transform.position);
            }
            public override void BindData()
            {
                data.position.Bind(SetPosition);
            }
            public override void UnbindData()
            {
                data.position.Unbind(SetPosition);
            }
        }
    }
}