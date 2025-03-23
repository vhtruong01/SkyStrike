using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class PhaseData : IGameData
        {
            public MoveData[] moveDataList;
            public FireData[] fireDataList;
        }
    }
}