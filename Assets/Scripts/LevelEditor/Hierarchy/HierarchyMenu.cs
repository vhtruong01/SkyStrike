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
                hierarchyUIGroupPool.selectDataCall = EventManager.SelectObject;
            }
            public override void Init() { }
            protected override void CreateObject(IEditorData data)
            {
                if (data is ObjectDataObserver objectData)
                    DisplayObject(objectData);
            }
            protected override void SelectObject(IEditorData data) => hierarchyUIGroupPool.SelectItem(data);
            private void DisplayObject(ObjectDataObserver objectData)
            {
                hierarchyUIGroupPool.CreateItem(objectData);
                var originalObject = hierarchyUIGroupPool.GetItem(objectData) as HierarchyItemUI;
                //
                //var refObject = hierarchyUIGroupPool.GetSelectedItem();
                //if (refObject != null)
                //{
                //    var refData = refObject.data as ObjectDataObserver;
                //    objectData.refId = refData.id;
                //    hierarchyUIGroupPool.MoveItemByIndex(originalObject.index.Value, refObject.index.Value + 1);
                //}
                //originalObject.SetPadding(waveDataObserver.GetHierarchyLevel(objectData.id));
            }
            //public void ReferenceObject(int index,int refIndex) => 
            protected override void SelectWave(IEditorData data)
            {
                var waveDataObserver = data as WaveDataObserver;
                hierarchyUIGroupPool.Clear();
                foreach (var objectData in waveDataObserver.objectList)
                    DisplayObject(objectData);
            }
            protected override void RemoveObject(IEditorData data)
            {
            }
        }
    }
}