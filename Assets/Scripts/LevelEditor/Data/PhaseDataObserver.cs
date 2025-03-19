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
            public ActionDataGroupObserver CreateEmpty()
            {
                ActionDataGroupObserver actionData = new();
                Add(actionData);
                return actionData;
            }
            public void Add(ActionDataGroupObserver data) => actionDataList.Add(data);
            public void Remove(ActionDataGroupObserver data) => actionDataList.Remove(data);
            public void Remove(int index) => actionDataList.RemoveAt(index);
            public void Swap(int leftIndex, int rightIndex) => actionDataList.Swap(leftIndex, rightIndex);
            public PhaseDataObserver Clone()
            {
                PhaseDataObserver newPhase = new();
                foreach (var actionData in actionDataList)
                    newPhase.actionDataList.Add(actionData.Clone());
                if (newPhase.actionDataList.Count == 0)
                    newPhase.CreateEmpty();
                return newPhase;
            }
            public IGameData ToGameData()
            {
                PhaseData phaseData = new();
                phaseData.actions = new ActionGroupData[actionDataList.Count];
                for (int i = 0; i < actionDataList.Count; i++)
                    phaseData.actions[i] = actionDataList[i].ToGameData() as ActionGroupData;
                return phaseData;
            }
        }
    }
}