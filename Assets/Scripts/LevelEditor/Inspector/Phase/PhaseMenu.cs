using SkyStrike.Enemy;
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
            [SerializeField] private List<ActionMenu> actionMenus;
            [SerializeField] private UIGroup switchActionButtonGroup;
            [SerializeField] private UIGroup actionUIGroup;
            [SerializeField] private Button addActionBtn;
            private EAction curActionType;
            private PhaseData phaseData;

            public void Awake()
            {
                for (int i = 0; i < actionMenus.Count; i++)
                {
                    Button switchButton = switchActionButtonGroup.CreateItem<Button>();
                    int index = i;
                    switchButton.GetComponentInChildren<TextMeshProUGUI>().text = actionMenus[i].type;
                    switchButton.onClick.AddListener(() => SelectActionType((EAction)index));
                }
                //select default
                //switchButtonGroup[0].
                addActionBtn.onClick.AddListener(AddEmptyAction);
                MenuManager.onSelectEnemy.AddListener(Display);
            }
            public void AddEmptyAction()
            {
                if (phaseData == null) return;
                AddAction(phaseData.AddAction(curActionType));
            }
            public void AddAction(IActionData actionData)
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
            }
            public void SelectActionType(EAction actionType)
            {
                actionUIGroup.Clear();
                curActionType = actionType;
                List<IActionData> actionDataList = phaseData.GetActionData(curActionType);
                foreach (IActionData actionData in actionDataList)
                    AddAction(actionData);
            }
            public bool SetData(IData data)
            {
                PhaseData newData = (data as EnemyData)?.phase;
                if (phaseData == newData) return false;
                phaseData = newData;
                return true;
            }
            public void Display(IData data)
            {
                if (!SetData(data)) return;
                if (phaseData != null)
                    SelectActionType(curActionType);
            }
            public bool CanDisplay() => phaseData != null;
        }
    }
}