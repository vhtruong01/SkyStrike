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
            public PhaseDataObserver(PhaseData phaseData) : this() => ImportData(phaseData);
            public List<ActionDataObserver> GetList() => actionDataList;
            public ActionDataObserver CreateEmpty()
            {
                ActionDataObserver actionData = CreateItem();
                if (actionData != null)
                    Add(actionData);
                return actionData;
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
            public void Add(ActionDataObserver data) => actionDataList.Add(data);
            public void Remove(ActionDataObserver data) => actionDataList.Remove(data);
            public void Remove(int index) => actionDataList.RemoveAt(index);
            public void Swap(int i1, int i2) => actionDataList.Swap(i1, i2);
            public void Set(int index, ActionDataObserver data) => actionDataList[index] = data;
            public PhaseDataObserver Clone()
            {
                PhaseDataObserver newPhase = new();
                foreach (var actionData in moveDataList)
                    newPhase.moveDataList.Add(actionData.Clone());
                foreach (var actionData in fireDataList)
                    newPhase.fireDataList.Add(actionData.Clone());
                return newPhase;
            }
            public PhaseData ExportData()
            {
                PhaseData phaseData = new()
                {
                    moveDataList = new MoveData[moveDataList.Count],
                    fireDataList = new FireData[fireDataList.Count]
                };
                for (int i = 0; i < moveDataList.Count; i++)
                    phaseData.moveDataList[i] = (moveDataList[i] as MoveDataObserver).ExportData();
                for (int i = 0; i < fireDataList.Count; i++)
                    phaseData.fireDataList[i] = (fireDataList[i] as FireDataObserver).ExportData();
                return phaseData;
            }
            public void ImportData(PhaseData phaseData)
            {
                if (phaseData == null) return;  
                for (int i = 0; i < phaseData.moveDataList.Length; i++)
                    moveDataList.Add(new MoveDataObserver(phaseData.moveDataList[i]));
                for (int i = 0; i < phaseData.fireDataList.Length; i++)
                    moveDataList.Add(new FireDataObserver(phaseData.fireDataList[i]));
            }
        }
    }
}