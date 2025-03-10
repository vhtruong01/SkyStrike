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
            private WaveDataObserver waveDataObserver;

            public override void Awake()
            {
                base.Awake();
                addObjectBtn.onClick.AddListener(() =>
                {
                    Collapse();
                    addObjectMenu.Expand();
                });
                EventManager.onCreateObject.AddListener(CreateObject);
                EventManager.onSelectObject.AddListener(SelectObject);
                EventManager.onSelectWave.AddListener(SelectWave);
                hierarchyUIGroupPool.selectDataCall = EventManager.SelectObject;
            }
            public override void Init() { }
            public void CreateObject(IEditorData data)
            {
                var objectData = data as ObjectDataObserver;
                if (objectData != null)
                    DisplayObject(objectData);
            }
            public void SelectObject(IEditorData data) => hierarchyUIGroupPool.SelectItem(data);
            private void DisplayObject(ObjectDataObserver objectData)
            {
                hierarchyUIGroupPool.CreateItem(objectData);
                var originalObject = hierarchyUIGroupPool.GetItem(objectData) as HierarchyItemUI;
                //
                var refObject = hierarchyUIGroupPool.GetSelectedItem();
                if (refObject != null)
                {
                    var refData = refObject.data as ObjectDataObserver;
                    objectData.refId = refData.id;
                    hierarchyUIGroupPool.MoveItemByIndex(originalObject.index.Value, refObject.index.Value + 1);
                }
                originalObject.SetPadding(waveDataObserver.GetHierarchyLevel(objectData.id));
            }
            //public void ReferenceObject(int index,int refIndex) => 
            public void SelectWave(IEditorData data)
            {
                waveDataObserver = data as WaveDataObserver;
                hierarchyUIGroupPool.Clear();
                foreach (var objectData in waveDataObserver.objectList)
                    DisplayObject(objectData);
            }
        }
    }
}