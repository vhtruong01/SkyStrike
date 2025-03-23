using UnityEngine;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class PhaseMenu : SubMenu<PhaseDataObserver>, IElementContainer<ActionDataObserver>
        {
            [SerializeField] private MoveActionMenu moveActionMenu;
            [SerializeField] private FireActionMenu fireActionMenu;
            [SerializeField] private UIGroup switchActionButtonGroup;
            private PhaseItemList actionUIGroupPool;
            private List<ActionMenu> actionMenus;
            private int curActionMenuIndex;

            public override void Init()
            {
                curActionMenuIndex = -1;
                actionMenus = new() { moveActionMenu, fireActionMenu };
                actionUIGroupPool = gameObject.GetComponent<PhaseItemList>();
                actionUIGroupPool.Init(SelectAction, SelectNullAction);
                for (int i = 0; i < switchActionButtonGroup.Count; i++)
                {
                    switchActionButtonGroup.GetBaseItem(i).onSelectUI.AddListener(ChangeActionType);
                    actionMenus[i].Hide();
                }
                switchActionButtonGroup.SelectFirstItem();
            }
            public void SelectNullAction()
            {
                if (curActionMenuIndex != -1)
                    actionMenus[curActionMenuIndex].Display(null);
            }
            private void SelectAction(ActionDataObserver data)
            {
                actionMenus[curActionMenuIndex].Display(data);
                actionMenus[curActionMenuIndex].Show();
            }
            private void ChangeActionType(int index)
            {
                if (index != curActionMenuIndex)
                {
                    UnbindData();
                    curActionMenuIndex = index;
                    if (data != null)
                        BindData();
                }
            }
            public override void BindData()
            {
                data.actionType = (EActionType)curActionMenuIndex;
                actionUIGroupPool.DisplayDataList();
            }
            public override void UnbindData() => actionUIGroupPool.Clear();
            public IDataList<ActionDataObserver> GetDataList() => data;
        }
    }
}