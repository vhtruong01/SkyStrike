using SkyStrike.Game;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class PhaseDataObserver : IDataList<ActionDataGroupObserver>, ICloneable<PhaseDataObserver>
        {
            private List<ActionDataGroupObserver> actionDataList;

            public PhaseDataObserver() => actionDataList = new();
            public List<ActionDataGroupObserver> GetList() => actionDataList;
            public ActionDataGroupObserver Create()
            {
                ActionDataGroupObserver actionData = new();
                actionDataList.Add(actionData);
                return actionData;
            }
            public void Remove(ActionDataGroupObserver data)
            {
                if (actionDataList.Count > 1)
                    actionDataList.Remove(data);
            }
            public void Swap(int leftIndex, int rightIndex)
            {
                if (leftIndex > 0 & rightIndex < actionDataList.Count)
                    (actionDataList[leftIndex], actionDataList[rightIndex]) = (actionDataList[leftIndex], actionDataList[rightIndex]);
            }
            public PhaseDataObserver Clone()
            {
                PhaseDataObserver newPhase = new();
                foreach (var actionData in actionDataList)
                    newPhase.actionDataList.Add(actionData.Clone());
                if (newPhase.actionDataList.Count == 0)
                    newPhase.Create();
                return newPhase;
            }
            public IGameData ToGameData()
            {
                PhaseData phaseData = new();
                phaseData.actions=new ActionGroupData[actionDataList.Count];
                for (int i = 0; i < actionDataList.Count; i++)
                    phaseData.actions[i] = actionDataList[i].ToGameData() as ActionGroupData;
                return phaseData;
            }
        }
    }
}