using System.Collections.Generic;

namespace SkyStrike
{
    namespace Enemy
    {
        public class PhaseData
        {
            public List<IActionData> moveDataList;
            public List<IActionData> fireDataList;

            public PhaseData()
            {
                moveDataList = new();
                fireDataList = new();
            }
            private PhaseData(PhaseData phase) : this()
            {
                for (int i = 0; i < phase.moveDataList.Count; i++)
                    moveDataList.Add(phase.moveDataList[i].Clone());
                for (int i = 0; i < phase.fireDataList.Count; i++)
                    fireDataList.Add(phase.fireDataList[i].Clone());
            }
            public PhaseData Clone() => new(this);
            public List<IActionData> GetActionData(EAction eAction)
            {
                return eAction switch
                {
                    EAction.Move => moveDataList,
                    EAction.Fire => fireDataList,
                    _ => null,
                };
            }
            public IActionData AddAction(EAction eAction)
            {
                IActionData actionData = CreateAction(eAction);
                if (actionData != null)
                {
                    var dataList = GetActionData(eAction);
                    dataList.Add(actionData);
                }
                return actionData;
            }
            private IActionData CreateAction(EAction eAction)
            {
                return eAction switch
                {
                    EAction.Move => new MoveData(),
                    EAction.Fire => new FireData(),
                    _ => null,
                };
            }
        }
    }
}