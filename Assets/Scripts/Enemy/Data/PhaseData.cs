using System.Collections.Generic;

namespace SkyStrike
{
    namespace Enemy
    {
        public class PhaseData
        {
            public List<MoveData> moveDataList;
            public List<FireData> fireDataList;

            public PhaseData()
            {
                moveDataList = new();
                fireDataList = new();
            }
            private PhaseData(PhaseData phase) : this()
            {
                for (int i = 0; i < phase.moveDataList.Count; i++)
                    moveDataList.Add(phase.moveDataList[i]);
                for (int i = 0; i < phase.fireDataList.Count; i++)
                    fireDataList.Add(phase.fireDataList[i]);
            }
            public PhaseData Clone() => new(this);
        }
    }
}