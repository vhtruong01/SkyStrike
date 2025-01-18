using SkyStrike.Enemy;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class PhaseMenu : MonoBehaviour
        {
            [SerializeField] private List<Button> switchButtons;
            [SerializeField] private List<ActionMenu> actionMenus;
            [SerializeField] private Button addActionBtn;
            //
            private int curActionType;
            private Dictionary<int, List<ActionUI>> phase;
            private EnemyData enemyData;

            public void Awake()
            {
                phase = new();
                for (int i = 0; i < switchButtons.Count; i++)
                {
                    switchButtons[i].onClick.AddListener(() => SelectActionType(i));
                    phase[i] = new List<ActionUI>();
                }
                addActionBtn.onClick.AddListener(AddAction);
                MenuManager.onSelectEnemy.AddListener(DisplayPhase);
            }
            public void Start()
            {
                SelectActionType(0);
            }
            public void SelectActionType(int actionType)
            {
                if (!phase.TryGetValue(actionType, out var actionList)) return;
                curActionType = actionType;
                //
            }
            public void AddAction()
            {
                //phase[curActionType].
            }
            public void RemoveAction()
            {
                //
            }
            public void DisplayPhase(IEnemyData enemyData)
            {
                this.enemyData = enemyData as EnemyData;
                gameObject.SetActive(this.enemyData != null);
                if (this.enemyData == null) return;
                //
            }
            public void DisplayAction(ActionUI actionUI)
            {
                actionMenus[curActionType].Display(actionUI);
            }
        }
    }
}
