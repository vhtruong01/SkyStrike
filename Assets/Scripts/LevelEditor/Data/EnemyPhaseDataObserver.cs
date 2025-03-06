using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyPhaseDataObserver : IDataList<EnemyActionDataGroupObserver>, ICloneable<EnemyPhaseDataObserver>
        {
            private List<EnemyActionDataGroupObserver> actionDataList;

            public EnemyPhaseDataObserver() => actionDataList = new();
            public List<EnemyActionDataGroupObserver> GetList() => actionDataList;
            public EnemyActionDataGroupObserver Create()
            {
                EnemyActionDataGroupObserver actionData = new();
                actionDataList.Add(actionData);
                return actionData;
            }
            public void Remove(EnemyActionDataGroupObserver data) => actionDataList.Remove(data);
            public void Swap(int leftIndex, int rightIndex)
            {
                if (leftIndex > 0 & rightIndex < actionDataList.Count)
                    (actionDataList[leftIndex], actionDataList[rightIndex]) = (actionDataList[leftIndex], actionDataList[rightIndex]);
            }
            public EnemyPhaseDataObserver Clone()
            {
                EnemyPhaseDataObserver newPhase = new();
                foreach (var actionData in actionDataList)
                    newPhase.actionDataList.Add(actionData.Clone());
                return newPhase;
            }
        }
    }
}