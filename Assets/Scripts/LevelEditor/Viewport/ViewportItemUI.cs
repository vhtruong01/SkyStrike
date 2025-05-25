using SkyStrike.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class ViewportItemUI : MoveableUIElement<ObjectDataObserver>
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image refIcon;
        [SerializeField] private Image itemIcon;

        public override void SetData(ObjectDataObserver data)
        {
            base.SetData(data);
            icon.sprite = data.metaData.data.sprite;
            icon.color = data.metaData.data.color;
            SetDropItem(data.dropItemType.data);
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
        public void SetDropItem(EItem itemType)
        {
            EventManager.GetItemMetaData(itemType, out var data);
            if (data == null) itemIcon.color = new();
            else
            {
                itemIcon.color = Color.white;
                itemIcon.sprite = data.sprite;
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
            data.dropItemType.Bind(SetDropItem);
        }
        public override void UnbindData()
        {
            data.position.Unbind(SetPosition);
            data.dropItemType.Unbind(SetDropItem);
        }
    }
}