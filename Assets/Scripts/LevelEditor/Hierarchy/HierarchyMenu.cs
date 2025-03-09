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
                //hierarchyUIGroupPool.selectDataCall = MenuManager.SelectObject;
                EventManager.onCreateObject.AddListener(CreateObject);
            }
            public void CreateObject(IEditorData data)
            {
                var objectData = (data as ObjectDataObserver).Clone();
                if (objectData == null) return;
                hierarchyUIGroupPool.CreateItem(out HierarchyItemUI obj);
                obj.SetData(objectData);
            }
        }
    }
}