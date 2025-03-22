using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class HierarchyMenu : Menu
        {
            [SerializeField] private Menu addObjectMenu;
            [SerializeField] private Button addObjectBtn;
            private HierarchyItemList hierarchyUIGroupPool;

            public override void Awake()
            {
                base.Awake();
                addObjectBtn.onClick.AddListener(() =>
                {
                    Collapse();
                    addObjectMenu.Expand();
                });
                EventManager.onSetRefObject.AddListener(ReferenceObject);
            }
            public override void Init()
            {
                hierarchyUIGroupPool = gameObject.GetComponent<HierarchyItemList>();
                hierarchyUIGroupPool.Init(EventManager.SelectObject);
            }
            protected override void CreateObject(ObjectDataObserver data) => hierarchyUIGroupPool.CreateItem(data);
            protected override void SelectObject(ObjectDataObserver data)
            {
                if (data == null) hierarchyUIGroupPool.SelectNone();
                else hierarchyUIGroupPool.SelectItem(data);
            }
            protected override void RemoveObject(ObjectDataObserver data)
            {
                var itemIndex = hierarchyUIGroupPool.GetItemIndex(data);
                var item = hierarchyUIGroupPool.GetItem(itemIndex) as HierarchyItemUI;
                var refItem = hierarchyUIGroupPool.GetItem(data.refData) as HierarchyItemUI;
                refItem?.RemoveChild(item);
                item.RemoveAllChildren();
                hierarchyUIGroupPool.RemoveItem(itemIndex);
            }
            protected override void SelectWave(WaveDataObserver data)
            {
                hierarchyUIGroupPool.Clear();
                Dictionary<ObjectDataObserver, Node> marked = new();
                foreach (var objectData in data.GetList())
                    CreateNode(objectData, marked);
                foreach (var node in marked)
                    if (node.Value.parent == null)
                        CreateUI(node.Value);
            }
            private void ReferenceObject(ObjectDataObserver refData)
            {
                if (!hierarchyUIGroupPool.TryGetValidSelectedIndex(out int curObjectIndex)) return;
                var curItem = hierarchyUIGroupPool.GetItem(curObjectIndex) as HierarchyItemUI;
                var curData = hierarchyUIGroupPool.GetSelectedItem().data;
                int newPos = -1;
                hierarchyUIGroupPool.SelectNone();
                HierarchyItemUI refItem;
                int refObjectIndex;
                if (curData.refData != null)
                {
                    refObjectIndex = hierarchyUIGroupPool.GetItemIndex(curData.refData);
                    refItem = hierarchyUIGroupPool.GetItem(refObjectIndex) as HierarchyItemUI;
                    if (refItem != null)
                        refItem.RemoveChild(curItem);
                }
                if (refData != null)
                {
                    refObjectIndex = hierarchyUIGroupPool.GetItemIndex(refData);
                    refItem = hierarchyUIGroupPool.GetItem(refObjectIndex) as HierarchyItemUI;
                    newPos = refObjectIndex + (refObjectIndex < curObjectIndex ? 0 : -1) + refItem.GetChildCount();
                    refItem.AddChild(curItem);
                }
                hierarchyUIGroupPool.MoveItemArray(ref curObjectIndex, newPos, curItem.GetChildCount());
                hierarchyUIGroupPool.SelectItem(curData);
            }
            private HierarchyItemUI CreateUI(Node node)
            {
                var itemUI = hierarchyUIGroupPool.CreateItem(node.data).gameObject.GetComponent<HierarchyItemUI>();
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
}