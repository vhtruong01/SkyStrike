using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class HierarchyItemUI : UIElement
        {
            private static readonly int paddingLen = 40;
            [SerializeField] private RectTransform spaceItem;
            [SerializeField] private TextMeshProUGUI objectName;
            [SerializeField] private TextMeshProUGUI objectId;

            public override void SetData(IEditorData data)
            {
                this.data = data;
                var objectDataObserver = this.data as ObjectDataObserver;
                objectName.text = objectDataObserver.name.data;
                //
                objectId.text = "id: " + objectDataObserver.id.ToString();
            }
            public void SetPadding(int level)
            {
                spaceItem.sizeDelta = new(paddingLen * level, spaceItem.sizeDelta.y);
            }
            public override void OnPointerClick(PointerEventData eventData) => InvokeData();
        }
    }
}