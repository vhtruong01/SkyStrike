using SkyStrike.Game;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class PhaseDataObserver : IDataList<ActionDataObserver>, IEditorData<PhaseData, PhaseDataObserver>
        {
            private readonly List<ActionDataObserver> moveDataList;
            private readonly List<ActionDataObserver> fireDataList;
            private List<ActionDataObserver> actionDataList;
            private EActionType _type;
            public EActionType actionType
            {
                get => _type;
                set
                {
                    _type = value;
                    switch (_type)
                    {
                        case EActionType.Move:
                            actionDataList = moveDataList;
                            break;
                        case EActionType.Fire:
                            actionDataList = fireDataList;
                            break;
                    }
                }
            }
            public PhaseDataObserver()
            {
                moveDataList = new();
                fireDataList = new();
            }
            public List<ActionDataObserver> GetList() => actionDataList;
            public ActionDataObserver CreateEmpty()
            {
                ActionDataObserver actionData = CreateItem();
                if (actionData != null)
                    Add(actionData);
                return actionData;
            }
            public void Add(ActionDataObserver data) => actionDataList.Add(data);
            public void Remove(ActionDataObserver data) => actionDataList.Remove(data);
            public void Remove(int index) => actionDataList.RemoveAt(index);
            public void Swap(int i1, int i2) => actionDataList.Swap(i1, i2);
            public void Clear() => actionDataList.Clear();
            public PhaseDataObserver Clone()
            {
                PhaseDataObserver newPhase = new();
                foreach (var actionData in moveDataList)
                    newPhase.moveDataList.Add(actionData.Clone());
                foreach (var actionData in fireDataList)
                    newPhase.fireDataList.Add(actionData.Clone());
                return newPhase;
            }
            public PhaseData ToGameData()
            {
                PhaseData phaseData = new();
                //phaseData.actions = new ActionGroupData[actionDataList.Count];
                //for (int i = 0; i < actionDataList.Count; i++)
                //    phaseData.actions[i] = actionDataList[i].ToGameData() as ActionGroupData;
                return phaseData;
            }
            private ActionDataObserver CreateItem()
            {
                switch (actionType)
                {
                    case EActionType.Move:
                        return new MoveDataObserver();
                    case EActionType.Fire:
                        actionDataList = fireDataList;
                        return new FireDataObserver();
                }
                return null;
            }
        }
    }
}