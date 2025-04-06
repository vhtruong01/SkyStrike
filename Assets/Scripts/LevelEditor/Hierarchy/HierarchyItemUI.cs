using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class HierarchyItemUI : UIElement<ObjectDataObserver>
        {
            private static readonly int paddingLen = 40;
            [SerializeField] private RectTransform spaceItem;
            [SerializeField] private TextMeshProUGUI itemId;
            private readonly HashSet<HierarchyItemUI> children = new();

            public override void SetData(ObjectDataObserver data)
            {
                base.SetData(data);
                spaceItem.sizeDelta = new(0, spaceItem.sizeDelta.y);
                children.Clear();
            }
            public override void Click() => InvokeData();
            public void RemoveChild(HierarchyItemUI child)
            {
                children.Remove(child);
                child.SetPadding(0);
            }
            public void RemoveAllChildren()
            {
                foreach (var child in children)
                {
                    child.data?.SetRefData(null);
                    child.SetPadding(0);
                }
                children.Clear();
            }
            public void AddChild(HierarchyItemUI newChild)
            {
                if (newChild == this || children.Contains(newChild))
                    throw new System.Exception("invalid child");
                children.Add(newChild);
                newChild.SetPadding(data.GetParentCount() + 1);
            }
            private void SetPadding(int parentCount)
            {
                spaceItem.sizeDelta = new(paddingLen * parentCount, spaceItem.sizeDelta.y);
                foreach (var child in children)
                    child.SetPadding(parentCount + 1);
            }
            public int GetChildCount()
            {
                int rs = 1;
                foreach (var child in children)
                    rs += child.GetChildCount();
                return rs;
            }
            public override void SetName(string name)
            {
                base.SetName(name);
                data.name.OnlySetData(name);
            }
            private void SetIdDisplay(int id) => itemId.text = "id: " + id.ToString();
            public override void BindData()
            {
                data.name.Bind(SetName);
                data.id.Bind(SetIdDisplay);
            }
            public override void UnbindData()
            {
                data.name.Unbind(SetName);
                data.id.Unbind(SetIdDisplay);
            }
        }
    }
}