using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace SkyStrike
{
    namespace Editor
    {
        public class PhaseMenu : MonoBehaviour, ISubMenu
        {
            [SerializeField] private MoveActionMenu moveActionMenu;
            [SerializeField] private FireActionMenu fireActionMenu;
            [SerializeField] private UIGroup switchActionButtonGroup;
            [SerializeField] private UIGroup actionUIGroup;
            [SerializeField] private Button addActionGroupBtn;
            [SerializeField] private Button addActionBtn;
            [SerializeField] private Button removeActionBtn;
            private List<ActionMenu> actionMenus;
            private EnemyPhaseDataObserver phaseData;
            private EActionType curActionType;
            private int curActionIndex;

            public void Start()
            {
                actionMenus = new() { moveActionMenu, fireActionMenu };
                for (int i = 0; i < actionMenus.Count; i++)
                {
                    Button switchButton = switchActionButtonGroup.CreateItem<Button>();
                    int index = i;
                    switchButton.GetComponentInChildren<TextMeshProUGUI>().text = actionMenus[i].type;
                    switchButton.onClick.AddListener(() => SelectActionMenu((EActionType)index));
                    actionMenus[i].Hide();
                }
                switchActionButtonGroup.SelectFirstItem();
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
                ActionUI actionUI = actionUIGroup.CreateItem<ActionUI>();
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
                actionUIGroup.SelectItem(actionUI.gameObject);
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
            public bool SetData(IData data)
            {
                if (data is not EnemyDataObserver newData) return false;
                var newPhaseData = newData.isMetaData ? null : newData.phase;
                if (phaseData == newPhaseData) return false;
                phaseData = newPhaseData;
                return true;
            }
            public void Display(IData data)
            {
                bool isNewData = SetData(data);
                if (isNewData)
                {
                    if (CanDisplay())
                    {
                        actionUIGroup.Clear();
                        var actionDataList = phaseData.GetActionDataList();
                        foreach (var actionData in actionDataList)
                            AddActionGroup(actionData);
                        DeSelectActionMenu();
                    }
                    else
                    {
                        Hide();
                        return;
                    }
                }
                Show();
            }
            public bool CanDisplay() => phaseData != null;
            public void Hide()
            {
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);
            }
            public void Show()
            {
                if (!gameObject.activeSelf && CanDisplay())
                    gameObject.SetActive(true);
            }
        }
    }
}