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
            private int curActionIndex;

            public void Awake()
            {
                actionMenus = new() { moveActionMenu, fireActionMenu };
            }
            public void Start()
            {
                for (int i = 0; i < switchActionButtonGroup.Count; i++)
                {
                    var switchButton = switchActionButtonGroup.GetItem(i);
                    int index = i;
                    switchButton.onClick.AddListener(() => SelectActionMenu((EActionType)index));
                    actionMenus[i].Hide();
                }
                addActionGroupBtn.onClick.AddListener(AddEmptyActionGroup);
                addActionBtn.onClick.AddListener(AddAction);
                removeActionBtn.onClick.AddListener(RemoveAction);
                DisableActionButton();
            }
            public void AddEmptyActionGroup()
            {
                if (phaseData == null) return;
                AddActionGroup(phaseData.AddActionGroup());
            }
            public void AddActionGroup(EnemyActionDataObserver actionData)
            {
                actionUIGroupPool.CreateItem(out ActionUI actionUI);
                actionUI.SetListener(SelectAction);
                actionUI.SetData(actionData);
            }
            public void RemoveActionGroup()
            {
                //
            }
            public void AddAction()
            {
                if (curActionIndex == -1) return;
                phaseData.AddActionData(curActionIndex, curActionType);
                SelectCurrentActionMenu();
            }
            public void RemoveAction()
            {
                if (curActionIndex == -1) return;
                phaseData.RemoveActionData(curActionIndex, curActionType);
                SelectCurrentActionMenu();
            }
            public void SelectAction(ActionUI actionUI)
            {
                curActionIndex = actionUI.actionData.index;
                SelectCurrentActionMenu();
            }
            public void DeSelectActionMenu()
            {
                curActionIndex = -1;
                SelectCurrentActionMenu();
            }
            public void DisableActionButton()
            {
                addActionBtn.gameObject.SetActive(false);
                removeActionBtn.gameObject.SetActive(false);
            }
            public void EnableActionButton(bool isEnable)
            {
                addActionBtn.gameObject.SetActive(isEnable);
                removeActionBtn.gameObject.SetActive(!isEnable);
            }
            public void SelectCurrentActionMenu() => SelectActionMenu(curActionType);
            public void SelectActionMenu(EActionType actionType)
            {
                if (curActionType != actionType)
                    actionMenus[(int)curActionType].Hide();
                curActionType = actionType;
                var curActionMenu = actionMenus[(int)curActionType];
                var actionData = phaseData.GetActionData(curActionIndex, curActionType);
                if (actionData != null)
                {
                    curActionMenu.Display(actionData);
                    curActionMenu.Show();
                }
                else actionMenus[(int)curActionType].Hide();
                if (phaseData.HasAction(curActionIndex))
                    EnableActionButton(actionData == null);
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
                    var actionDataList = phaseData.GetActionDataList();
                    foreach (var actionData in actionDataList)
                        AddActionGroup(actionData);
                    DeSelectActionMenu();
                }
            }
            public override bool CanDisplay() => phaseData != null;
        }
    }
}