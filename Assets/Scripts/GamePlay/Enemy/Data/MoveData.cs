using System;
using UnityEngine;

namespace SkyStrike
{
    namespace Enemy
    {
        public class MoveData : IActionData
        {
            public Vector2 distance;
            public float rotation;
            public float speed;
            public float delay;
            public bool isSyncRot;

            public IActionData Clone()
            {
                return MemberwiseClone() as MoveData;
            }

            public string GetActionDataInfo(int index)
            {
                return index switch
                {
                    0 => distance.ToString(),
                    1 => rotation.ToString(),
                    _ => "",
                };
            }
        }
    }
}