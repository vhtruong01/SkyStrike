using System;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class ActionGroupData : IGameData
        {
            public MoveData moveData;
            public FireData fireData;
        }
    }
}