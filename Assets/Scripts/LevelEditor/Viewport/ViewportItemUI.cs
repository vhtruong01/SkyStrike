using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class ViewportItemUI : MoveableUIElement<ObjectDataObserver>
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
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            data.SetPosition(screen.GetActualPosition(transform.position));
        }
        public override void Click() => InvokeData();
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