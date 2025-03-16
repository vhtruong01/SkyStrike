using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class PhaseMenu : SubMenu
        {
            [SerializeField] private MoveActionMenu moveActionMenu;
            [SerializeField] private FireActionMenu fireActionMenu;
            [SerializeField] private UIGroup switchActionButtonGroup;
            [SerializeField] private UIGroupPool actionUIGroupPool;
            [SerializeField] private Button addActionGroupBtn;
            [SerializeField] private Button removeActionGroupBtn;
            [SerializeField] private Button moveUpActionGroupBtn;
            [SerializeField] private Button moveDownActionGroupBtn;
            private List<ActionMenu> actionMenus;
            private PhaseDataObserver phaseData;
            private EActionType curActionType;

            public void Awake()
            {
                curActionType = EActionType.Move;
                actionMenus = new() { moveActionMenu, fireActionMenu };
                addActionGroupBtn.onClick.AddListener(AddEmptyActionGroup);
                removeActionGroupBtn.onClick.AddListener(RemoveActionGroup);
                moveUpActionGroupBtn.onClick.AddListener(MoveUpActionGroup);
                moveDownActionGroupBtn.onClick.AddListener(MoveDownActionGroup);
                actionUIGroupPool.selectDataCall = SelectAndSetDataActionMenu;
            }
            public void Start()
            {
                for (int i = 0; i < switchActionButtonGroup.Count; i++)
                {
                    switchActionButtonGroup.GetItem(i).onSelectUI.AddListener(SelectActionMenu);
                    actionMenus[i].Hide();
                }
                actionUIGroupPool.SelectFirstItem();
            }
            public void AddEmptyActionGroup()
            {
                if (phaseData == null) return;
                AddActionGroup(phaseData.Create());
            }
            public void RemoveActionGroup()
            {
                var selectedActionGroup = actionUIGroupPool.GetSelectedItem() as ActionItemUI;
                if (selectedActionGroup != null)
                {
                    actionUIGroupPool.RemoveSelectedItem();
                    phaseData.Remove(selectedActionGroup.data as ActionDataGroupObserver);
                }
            }
            public void MoveUpActionGroup()
            {
                if (actionUIGroupPool.TryGetValidSelectedIndex(out var index))
                {
                    actionUIGroupPool.MoveLeftSelectedItem();
                    phaseData.Swap(index - 1, index);
                }
            }
            public void MoveDownActionGroup()
            {
                if (actionUIGroupPool.TryGetValidSelectedIndex(out var index))
                {
                    actionUIGroupPool.MoveRightSelectedItem();
                    phaseData.Swap(index, index + 1);
                }
            }
            private void AddActionGroup(ActionDataGroupObserver actionData)
            {
                actionUIGroupPool.CreateItem(actionData);
            }
            private void SelectAndSetDataActionMenu(IEditorData data)
            {
                ActionDataGroupObserver actionDataGroupObserver = data as ActionDataGroupObserver;
                for (int i = 0; i < actionMenus.Count; i++)
                    actionMenus[i].Display(actionDataGroupObserver?.GetActionData((EActionType)i));
                SelectCurrentActionMenu();
            }
            public void SelectCurrentActionMenu()
                => switchActionButtonGroup.SelectAndInvokeItem((int)curActionType);
            private void SelectActionMenu(int index) => SelectActionMenu((EActionType)index);
            private void SelectActionMenu(EActionType actionType)
            {
                var curMenu = actionMenus[(int)curActionType];
                if (curActionType != actionType || !curMenu.gameObject.activeSelf)
                {
                    curMenu.Hide();
                    curActionType = actionType;
                    curMenu = actionMenus[(int)curActionType];
                    curMenu.Show();
                }
            }
            public override bool SetData(IEditorData data)
            {
                var newData = data as ObjectDataObserver;
                var newPhaseData = newData == null || newData.isMetaData ? null : newData.phase;
                if (phaseData == newPhaseData) return false;
                phaseData = newPhaseData;
                return true;
            }
            public override void Display(IEditorData data)
            {
                bool isNewData = SetData(data);
                if (!CanDisplay())
                {
                    actionUIGroupPool.Clear();
                    Hide();
                    return;
                }
                if (isNewData)
                {
                    actionUIGroupPool.Clear();
                    var actionDataList = phaseData.GetList();
                    foreach (var actionData in actionDataList)
                        AddActionGroup(actionData);
                    actionUIGroupPool.SelectFirstItem();
                }
            }
            public override bool CanDisplay() => phaseData != null;
        }
    }
}