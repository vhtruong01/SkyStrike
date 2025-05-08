using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class HierarchyMenu : EventNotifyMenu
    {
        [SerializeField] private Menu addObjectMenu;
        [SerializeField] private Button addObjectBtn;
        private HierarchyItemList group;

        protected override void Preprocess()
        {
            base.Preprocess();
            EventManager.onSetRefObject.AddListener(DisplayReferenceObject);
            addObjectBtn.onClick.AddListener(() =>
            {
                Hide();
                addObjectMenu.Show();
            });
            group = gameObject.GetComponent<HierarchyItemList>();
            group.Init(EventManager.SelectObject);
        }
        private HierarchyItemUI GetItem(int index)
            => group.GetItem(index) as HierarchyItemUI;
        private HierarchyItemUI GetItem(ObjectDataObserver data)
            => group.GetItem(data) as HierarchyItemUI;
        protected override void CreateObject(ObjectDataObserver data)
        {
            group.CreateItem(data);
            DisplayReferenceObject(data, data.refData);
        }
        protected override void SelectObject(ObjectDataObserver data)
        {
            if (data == null) group.SelectNone();
            else group.SelectItem(data);
        }
        protected override void RemoveObject(ObjectDataObserver data)
        {
            var itemIndex = group.GetItemIndex(data);
            var item = GetItem(itemIndex);
            var refItem = GetItem(data.refData);
            refItem?.RemoveChild(item);
            item.RemoveAllChildren();
            group.RemoveItem(itemIndex);
        }
        protected override void SelectWave(WaveDataObserver data)
        {
            group.Clear();
            Dictionary<ObjectDataObserver, Node> marked = new();
            data.GetList(out var list);
            foreach (var objectData in list)
                CreateNode(objectData, marked);
            foreach (var node in marked)
                if (node.Value.parent == null)
                    CreateUI(node.Value);
        }
        private void DisplayReferenceObject(ObjectDataObserver curData, ObjectDataObserver refData)
        {
            if (curData == null || (curData.refData == null && refData == null)) return;
            var selectedItemData = group.GetSelectedItem().data;
            var curObjectIndex = group.GetItemIndex(curData);
            var curItem = GetItem(curObjectIndex);
            int newPos = -1;
            group.SelectNone();
            HierarchyItemUI refItem;
            int refObjectIndex;
            if (curData.refData != null && curData.refData != refData)
            {
                refObjectIndex = group.GetItemIndex(curData.refData);
                refItem = GetItem(refObjectIndex);
                if (refItem != null)
                    refItem.RemoveChild(curItem);
            }
            if (refData != null)
            {
                refObjectIndex = group.GetItemIndex(refData);
                refItem = GetItem(refObjectIndex);
                newPos = refObjectIndex + (refObjectIndex < curObjectIndex ? 0 : -1) + refItem.GetChildCount();
                refItem.AddChild(curItem);
            }
            group.MoveItemArray(ref curObjectIndex, newPos, curItem.GetChildCount());
            group.SelectItem(selectedItemData);
        }
        private void DisplayReferenceObject(ObjectDataObserver refData)
        {
            var selectedItemData = group.GetSelectedItem()?.data;
            DisplayReferenceObject(selectedItemData, refData);
        }
        private HierarchyItemUI CreateUI(Node node)
        {
            var itemUI = group.CreateItem(node.data).gameObject.GetComponent<HierarchyItemUI>();
            for (int i = 0; i < node.children.Count; i++)
                itemUI.AddChild(CreateUI(node.children[i]));
            return itemUI;
        }
        private Node CreateNode(ObjectDataObserver data, Dictionary<ObjectDataObserver, Node> marked)
        {
            if (!marked.TryGetValue(data, out Node node))
            {
                node = new(data);
                if (data.refData != null)
                {
                    Node parent = CreateNode(data.refData, marked);
                    parent.children.Add(node);
                    node.parent = parent;
                    node.level = parent.level + 1;
                }
                marked.Add(data, node);
            }
            return node;
        }
        private class Node
        {
            public int level = 0;
            public ObjectDataObserver data;
            public Node parent;
            public List<Node> children;

            public Node(ObjectDataObserver data)
            {
                this.data = data;
                children = new();
            }
        }
    }
}