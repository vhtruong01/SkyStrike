using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
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
            [SerializeField] private UIGroupPool hierarchyUIGroupPool;

            public override void Awake()
            {
                base.Awake();
                addObjectBtn.onClick.AddListener(() =>
                {
                    Collapse();
                    addObjectMenu.Expand();
                });
                EventManager.onSetRefObject.AddListener(ReferenceObject);
                hierarchyUIGroupPool.selectDataCall = EventManager.SelectObject;
            }
            public override void Init() { }
            protected override void CreateObject(IEditorData data)
            {
                if (data is ObjectDataObserver objectData)
                    hierarchyUIGroupPool.CreateItem(objectData);
            }
            protected override void SelectObject(IEditorData data) => hierarchyUIGroupPool.SelectItem(data);
            protected override void RemoveObject(IEditorData data)
            {
                //
            }
            protected override void SelectWave(IEditorData data)
            {
                var waveDataObserver = data as WaveDataObserver;
                hierarchyUIGroupPool.Clear();
                Dictionary<ObjectDataObserver, Node> marked = new();
                foreach (var objectData in waveDataObserver.GetList())
                    CreateNode(objectData, marked);
                foreach (var node in marked)
                    if (node.Value.parent == null)
                        CreateUI(node.Value);
            }
            private void ReferenceObject(IEditorData data)
            {
                if (!hierarchyUIGroupPool.TryGetValidSelectedIndex(out int curObjectIndex)) return;
                HierarchyItemUI refItem = null;
                int refObjectIndex = -1;
                var refData = data as ObjectDataObserver;
                var curItem = hierarchyUIGroupPool.GetItem(curObjectIndex) as HierarchyItemUI;
                var curData = hierarchyUIGroupPool.GetSelectedItem().data as ObjectDataObserver;
                int newPos = -1;
                hierarchyUIGroupPool.SelectNone();
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
                hierarchyUIGroupPool.CreateItem(out HierarchyItemUI itemUI, node.data);
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