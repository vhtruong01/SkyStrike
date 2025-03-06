using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyPhaseDataObserver : ICloneable<EnemyPhaseDataObserver>
        {
            private List<EnemyActionDataGroupObserver> actionDataList;

            public EnemyPhaseDataObserver() => actionDataList = new();
            public List<EnemyActionDataGroupObserver> GetActionDataList() => actionDataList;
            public void RemoveActionGroup(EnemyActionDataGroupObserver actionGroup) => actionDataList.Remove(actionGroup);
            public EnemyActionDataGroupObserver CreateActionGroup()
            {
                EnemyActionDataGroupObserver actionData = new();
                actionDataList.Add(actionData);
                return actionData;
            }
            public EnemyPhaseDataObserver Clone()
            {
                EnemyPhaseDataObserver newPhase = new();
                foreach (var actionData in actionDataList)
                {
                    newPhase.actionDataList.Add(actionData.Clone());
                    //
                }
                return newPhase;
            }
        }
    }
}