using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class PhaseMenu : SubMenu, IArrangeable
        {
            [SerializeField] private MoveActionMenu moveActionMenu;
            [SerializeField] private FireActionMenu fireActionMenu;
            [SerializeField] private UIGroup switchActionButtonGroup;
            [SerializeField] private UIGroupPool actionUIGroupPool;
            [SerializeField] private Button addActionGroupBtn;
            [SerializeField] private Button duplicateActionGroupBtn;
            [SerializeField] private Button removeActionGroupBtn;
            [SerializeField] private Button moveUpActionGroupBtn;
            [SerializeField] private Button moveDownActionGroupBtn;
            private List<ActionMenu> actionMenus;
            private ObjectDataObserver objectData;
            private EActionType curActionType;

            public void Awake()
            {
                curActionType = EActionType.Move;
                actionMenus = new() { moveActionMenu, fireActionMenu };
                addActionGroupBtn.onClick.AddListener(Create);
                duplicateActionGroupBtn.onClick.AddListener(Duplicate);
                removeActionGroupBtn.onClick.AddListener(Remove);
                moveUpActionGroupBtn.onClick.AddListener(MoveLeft);
                moveDownActionGroupBtn.onClick.AddListener(MoveRight);
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
            public override bool CanDisplay() => objectData?.phase != null;
            public override bool SetData(IEditorData data)
            {
                var newData = data as ObjectDataObserver;
                if (newData != null && newData.isMetaData)
                    newData = null;
                if (objectData == newData) return false;
                objectData = newData;
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
                //if (isNewData)
                //{
                //    actionUIGroupPool.Clear();
                //    var actionDataList = objectData.phase.GetList();
                //    foreach (var actionData in actionDataList)
                //        Create(actionData);
                //    actionUIGroupPool.SelectFirstItem();
                //}
            }
            public void Create() => Create(null);
            private void Create(ActionDataGroupObserver actionData)
            {
                if (actionData == null)
                    objectData.phase.CreateEmpty();
                else objectData.phase.Add(actionData);
                actionUIGroupPool.CreateItem(actionData);
            }
            public void Remove()
            {
                var selectedActionGroup = actionUIGroupPool.GetSelectedItem() as ActionItemUI;
                if (selectedActionGroup != null)
                {
                    actionUIGroupPool.RemoveSelectedItem();
                    objectData.phase.Remove(selectedActionGroup.index.Value);
                }
            }
            public void MoveLeft()
            {
                if (actionUIGroupPool.TryGetValidSelectedIndex(out var index))
                {
                    actionUIGroupPool.MoveLeftSelectedItem();
                    objectData.phase.Swap(index - 1, index);
                }
            }
            public void MoveRight()
            {
                if (actionUIGroupPool.TryGetValidSelectedIndex(out var index))
                {
                    actionUIGroupPool.MoveRightSelectedItem();
                    objectData.phase.Swap(index, index + 1);
                }
            }
            public void Duplicate() { }// => Create(.);
            private void SelectAndSetDataActionMenu(IEditorData data)
            {
                ActionDataGroupObserver actionDataGroupObserver = data as ActionDataGroupObserver;
                for (int i = 0; i < actionMenus.Count; i++)
                    actionMenus[i].Display(actionDataGroupObserver?.GetActionData((EActionType)i));
                switchActionButtonGroup.SelectAndInvokeItem((int)curActionType);
            }
            private void SelectActionMenu(int index)
            {
                EActionType actionType = (EActionType)index;
                var curMenu = actionMenus[(int)curActionType];
                if (curActionType != actionType || !curMenu.gameObject.activeSelf)
                {
                    curMenu.Hide();
                    curActionType = actionType;
                    curMenu = actionMenus[(int)curActionType];
                    curMenu.Show();
                }
            }
            //
            public override void BindData()
            {
            }
            public override void UnbindData()
            {
            }
        }
    }
}