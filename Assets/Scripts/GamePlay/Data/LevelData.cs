using System;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [Serializable]
        public class LevelData : IGameData
        {
            public int star;
            public string name;
            public WaveData[] waves;
        }
    }
}