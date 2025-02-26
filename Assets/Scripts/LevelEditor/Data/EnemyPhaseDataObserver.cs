using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyPhaseDataObserver : ICloneable<EnemyPhaseDataObserver>
        {
            private List<EnemyActionDataObserver> actionDataList;

            public EnemyPhaseDataObserver() => actionDataList = new();
            public IData GetActionData(int index, EActionType actionType)
            {
                return HasAction(index) ? actionDataList[index].GetActionData(actionType) : null;
            }
            public bool HasAction(int index)
            {
                return index >= 0 & index < actionDataList.Count;
            }
            public void AddActionData(int index, EActionType actionType)
            {
                actionDataList[index].AddActionData(actionType);
            }
            public void RemoveActionData(int index, EActionType actionType)
            {
                actionDataList[index].RemoveActionData(actionType);
            }
            public List<EnemyActionDataObserver> GetActionDataList() => actionDataList;
            public EnemyActionDataObserver AddActionGroup()
            {
                EnemyActionDataObserver actionData = new();
                actionData.index = actionDataList.Count;
                actionDataList.Add(actionData);
                return actionData;
            }
            public void RemoveActionGroup()
            {
                //
            }
            public EnemyPhaseDataObserver Clone()
            {
                EnemyPhaseDataObserver newPhase = new();
                newPhase.actionDataList = new();
                foreach (var actionData in actionDataList)
                {
                    //
                    newPhase.actionDataList.Add(actionData.Clone());
                }
                return newPhase;
            }
        }
    }
}