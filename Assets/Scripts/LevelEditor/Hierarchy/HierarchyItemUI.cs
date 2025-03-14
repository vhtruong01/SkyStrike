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
            [SerializeField] private TextMeshProUGUI itemName;
            [SerializeField] private TextMeshProUGUI itemId;

            public override void SetData(IEditorData data)
            {
                var objectDataObserver = this.data as ObjectDataObserver;
                objectDataObserver?.name.Unbind(ChangeName);
                this.data = data;
                objectDataObserver = this.data as ObjectDataObserver;
                objectDataObserver.name.Bind(ChangeName);
                itemId.text = "id: " + objectDataObserver.id.ToString();
            }
            public void SetPadding(int level)
            {
                spaceItem.sizeDelta = new(paddingLen * level, spaceItem.sizeDelta.y);
            }
            public override void OnPointerClick(PointerEventData eventData) => InvokeData();
            private void ChangeName(string name) => itemName.text = name;
        }
    }
}