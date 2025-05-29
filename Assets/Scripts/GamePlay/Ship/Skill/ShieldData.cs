using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "Shield", menuName = "Data/Shield")]
    public class ShieldData : SkillData
    {
        [field: SerializeField, ReadOnly] public float duration { get; private set; }
        [field: SerializeField] public List<float> durationList { get; set; }

        protected override void Upgrade()
        {
            if (lv < durationList.Count)
                duration = durationList[lv];
        }
    }
}