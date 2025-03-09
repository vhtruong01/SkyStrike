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
                hierarchyUIGroupPool.CreateItem(out HierarchyItemUI obj, objectData);
                //
            }
            public void SelectWave(IEditorData data)
            {
                var waveDataObserver = data as WaveDataObserver;
                //objectUIGroupPool.Clear();
                //foreach (var objectData in waveDataObserver.objectList)
                //    DisplayObject(objectData);
            }
        }
    }
}