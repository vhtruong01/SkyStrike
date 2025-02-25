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
            [SerializeField] private Button addActionBtn;
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
                    switchButton.onClick.AddListener(() => SelectActionType((EActionType)index));
                    actionMenus[i].Hide();
                }
                switchActionButtonGroup.SelectFirstItem();
                addActionBtn.onClick.AddListener(AddEmptyAction);
            }
            public void AddEmptyAction()
            {
                if (phaseData == null) return;
                AddAction(phaseData.AddAction(curActionType));
            }
            public void AddAction(IEnemyActionDataObserver actionData)
            {
                ActionUI actionUI = actionUIGroup.CreateItem<ActionUI>();
                actionUI.SetListener(SelectAction);
                actionUI.SetData(actionData);
            }
            public void RemoveAction()
            {
                //
            }
            public void SelectAction(ActionUI actionUI)
            {
                actionUIGroup.SelectItem(actionUI.gameObject);
                curActionIndex = actionUI.actionData.index;
                SelectActionType(curActionType);
            }
            public void DeselectActionType()
            {
                curActionIndex = -1;
                SelectActionType(curActionType);
            }
            public void SelectActionType(EActionType actionType)
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
                if (isNewData && CanDisplay())
                {
                    actionUIGroup.Clear();
                    var actionDataList = phaseData.GetActionDataArray(curActionType);
                    foreach (var actionData in actionDataList)
                        AddAction(actionData);
                    DeselectActionType();
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