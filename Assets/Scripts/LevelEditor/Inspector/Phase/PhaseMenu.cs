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
                addActionBtn.onClick.AddListener(AddAction);
                removeActionBtn.onClick.AddListener(RemoveAction);
                DisableActionButton();
                //
            }
            public void Start()
            {
                for (int i = 0; i < switchActionButtonGroup.Count; i++)
                {
                    var switchButton = switchActionButtonGroup.GetItem(i);
                    switchButton.onSelectUI.AddListener(SelectActionMenu);
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
                AddActionGroup(phaseData.CreateActionGroup());
            }
            public void RemoveActionGroup()
            {
                var selectedActionGroup = actionUIGroupPool.GetSelectedItem() as ActionUI;
                if (selectedActionGroup != null)
                {
                    actionUIGroupPool.RemoveSelectedItem();
                    phaseData.RemoveActionGroup(selectedActionGroup.actionDataGroup);
                }
            }
            private void AddActionGroup(EnemyActionDataGroupObserver actionData)
            {
                actionUIGroupPool.CreateItem(out ActionUI actionUI);
                actionUI.SetData(actionData);
            }
            public void AddAction()
            {
                actionUIGroupPool.GetSelectedItemComponent<ActionUI>().actionDataGroup.AddActionData(curActionType);
                SelectCurrentActionMenu();
            }
            public void RemoveAction()
            {
                //
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
                //if (curActionType != actionType)
                //    actionMenus[(int)curActionType].Hide();
                //curActionType = actionType;
                //var curActionMenu = actionMenus[(int)curActionType];

                //if ()
                //    EnableActionButton(curActionMenu.CanDisplay());
                //else DisableActionButton();

                //var actionGroup = actionUIGroupPool.GetSelectedItemComponent<ActionUI>();
                //if (actionGroup != null)
                //{
                //    var actionData = actionGroup.actionData.GetActionData(curActionType);
                //    if (actionData != null)
                //    {
                //        curActionMenu.Display(actionData);
                //        curActionMenu.Show();
                //    }
                //    else actionMenus[(int)curActionType].Hide();
                //}
                //else
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
                    var actionDataList = phaseData.GetActionDataList();
                    foreach (var actionData in actionDataList)
                        AddActionGroup(actionData);
                    SelectAndSetDataActionMenu(null);
                }
            }
            public override bool CanDisplay() => phaseData != null;
        }
    }
}