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
            public void ReferenceObject(IEditorData data)
            {
                if (!hierarchyUIGroupPool.TryGetValidSelectedIndex(out int curObjectIndex)) return;

                HierarchyItemUI refItem = null;
                int refObjectIndex = -1;
                var refData = data as ObjectDataObserver;
                var curItem = hierarchyUIGroupPool.GetItem(curObjectIndex) as HierarchyItemUI;
                var curData = hierarchyUIGroupPool.GetSelectedItem().data as ObjectDataObserver;
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
                    refItem.SetChild(curItem);
                }
                else refObjectIndex = -1;
                hierarchyUIGroupPool.MoveItemArray(curObjectIndex, refObjectIndex, curItem.GetChildCount() + 1);
                hierarchyUIGroupPool.SelectItem(curData);
            }

            protected override void SelectWave(IEditorData data)
            {
                //
                var waveDataObserver = data as WaveDataObserver;
                hierarchyUIGroupPool.Clear();
                foreach (var objectData in waveDataObserver.objectList)
                    hierarchyUIGroupPool.CreateItem(objectData);
            }
            protected override void RemoveObject(IEditorData data)
            {
            }
        }
    }
}