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
            private EActionType curActionType;
            private EnemyPhaseDataObserver phaseData;

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
            public void OnEnable()
            {
                if (!CanDisplay()) return;
                SelectActionType(curActionType);
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
                actionUI.Display(actionData, curActionType);
            }
            public void RemoveAction()
            {
                //
            }
            public void SelectAction(ActionUI actionUI)
            {
                actionUIGroup.SelectItem(actionUI.gameObject);
                var curActionMenu = actionMenus[(int)curActionType];
                curActionMenu.Display(actionUI.actionData);
                curActionMenu.Show();
            }
            public void SelectActionType(EActionType actionType)
            {
                if (!CanDisplay()) return;
                actionUIGroup.Clear();
                actionMenus[(int)curActionType].Hide();
                curActionType = actionType;
                var actionDataList = phaseData.GetActionDataArray(curActionType);
                foreach (var actionData in actionDataList)
                    AddAction(actionData);
            }
            public bool SetData(IData data)
            {
                var newData = (data as EnemyDataObserver)?.phase;
                if (phaseData == newData) return false;
                phaseData = newData;
                return true;
            }
            public void Display(IData data)
            {
                bool isNewData = SetData(data);
                if (isNewData && CanDisplay())
                    SelectActionType(curActionType);
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