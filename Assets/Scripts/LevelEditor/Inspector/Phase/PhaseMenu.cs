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
            [SerializeField] private Button addActionBtn;
            [SerializeField] private Button removeActionBtn;
            private List<ActionMenu> actionMenus;
            private EnemyPhaseDataObserver phaseData;
            private EActionType curActionType;

            public void Awake()
            {
                curActionType = 0;
                actionMenus = new() { moveActionMenu, fireActionMenu };
                addActionGroupBtn.onClick.AddListener(AddEmptyActionGroup);
                removeActionGroupBtn.onClick.AddListener(RemoveActionGroup);
                moveUpActionGroupBtn.onClick.AddListener(MoveUpActionGroup);
                moveDownActionGroupBtn.onClick.AddListener(MoveDownActionGroup);
                addActionBtn.onClick.AddListener(AddAction);
                removeActionBtn.onClick.AddListener(RemoveAction);
                DisableActionButton();
                actionUIGroupPool.selectDataCall = SelectAndSetDataActionMenu;
            }
            public void Start()
            {
                for (int i = 0; i < switchActionButtonGroup.Count; i++)
                {
                    switchActionButtonGroup.GetItem(i).onSelectUI.AddListener(SelectActionMenu);
                    actionMenus[i].Hide();
                }
            }
            private void DisableActionButton()
            {
                addActionBtn.gameObject.SetActive(false);
                removeActionBtn.gameObject.SetActive(false);
            }
            private void EnableActionButton(bool isEnable)
            {
                addActionBtn.gameObject.SetActive(isEnable);
                removeActionBtn.gameObject.SetActive(!isEnable);
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
                    phaseData.Remove(selectedActionGroup.actionDataGroup);
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
            private void AddActionGroup(EnemyActionDataGroupObserver actionData)
            {
                actionUIGroupPool.CreateItem(out ActionItemUI actionUI);
                actionUI.SetData(actionData);
            }
            public void AddAction()
            {
                var actionUI = actionUIGroupPool.GetSelectedItemComponent<ActionItemUI>();
                actionUI.actionDataGroup.AddActionData(curActionType);
                var actionData = actionUI.actionDataGroup.GetActionData(curActionType);
                actionMenus[(int)curActionType].Display(actionData);
                SelectCurrentActionMenu();
            }
            public void RemoveAction()
            {
                actionUIGroupPool.GetSelectedItemComponent<ActionItemUI>().actionDataGroup.RemoveActionData(curActionType);
                actionMenus[(int)curActionType].Display(null);
                SelectCurrentActionMenu();
            }
            private void SelectAndSetDataActionMenu(IData data)
            {
                EnemyActionDataGroupObserver enemyActionDataGroupObserver = data as EnemyActionDataGroupObserver;
                for (int i = 0; i < actionMenus.Count; i++)
                    actionMenus[i].Display(enemyActionDataGroupObserver?.GetActionData((EActionType)i));
                SelectCurrentActionMenu();
            }
            public void SelectCurrentActionMenu()
                => switchActionButtonGroup.SelectAndInvoke((int)curActionType);
            private void SelectActionMenu(int index) => SelectActionMenu((EActionType)index);
            private void SelectActionMenu(EActionType actionType)
            {
                if (curActionType != actionType)
                {
                    actionMenus[(int)curActionType].Hide();
                    curActionType = actionType;
                }
                var curActionMenu = actionMenus[(int)curActionType];
                if (actionUIGroupPool.GetSelectedItem() != null)
                {
                    bool canDisplay = curActionMenu.CanDisplay();
                    EnableActionButton(!canDisplay);
                    if (!canDisplay)
                        curActionMenu.Hide();
                    else curActionMenu.Show();
                }
                else DisableActionButton();
            }
            public override bool SetData(IData data)
            {
                EnemyDataObserver newData = data as EnemyDataObserver;
                var newPhaseData = newData == null || newData.isMetaData ? null : newData.phase;
                if (phaseData == newPhaseData) return false;
                phaseData = newPhaseData;
                return true;
            }
            public override void Display(IData data)
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
                    SelectAndSetDataActionMenu(null);
                }
            }
            public override bool CanDisplay() => phaseData != null;
        }
    }
}