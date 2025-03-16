using System.Collections.Generic;
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
            private readonly HashSet<HierarchyItemUI> children = new();

            public override void SetData(IEditorData data)
            {
                var objectDataObserver = this.data as ObjectDataObserver;
                objectDataObserver?.name.Unbind(ChangeName);
                this.data = data;
                objectDataObserver = this.data as ObjectDataObserver;
                objectDataObserver.name.Bind(ChangeName);
                itemId.text = "id: " + objectDataObserver.id.ToString();
                spaceItem.sizeDelta = new(0, spaceItem.sizeDelta.y);
                children.Clear();
            }
            public void RemoveChild(HierarchyItemUI child)
            {
                children.Remove(child);
                child.SetPadding(0);
            }
            public void AddChild(HierarchyItemUI newChild)
            {
                if (newChild == this || children.Contains(newChild))
                    throw new System.Exception("invalid child");
                children.Add(newChild);
                newChild.SetPadding((data as ObjectDataObserver).GetParentCount() + 1);
            }
            private void SetPadding(int parentCount)
            {
                spaceItem.sizeDelta = new(paddingLen * parentCount, spaceItem.sizeDelta.y);
                foreach (var child in children)
                    child.SetPadding(parentCount + 1);
            }
            public int GetChildCount() => children.Count;
            public override void OnPointerClick(PointerEventData eventData) => InvokeData();
            private void ChangeName(string name) => itemName.text = name;
        }
    }
}