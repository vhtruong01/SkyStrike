using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "Laser", menuName = "Data/Laser")]
    public class LaserData : SkillData
    {
        [field: SerializeField] public float damageInterval { get; private set; } = 0.25f;
        [field: SerializeField,ReadOnly] public float len { get; private set; } = 12;
        [field: SerializeField,ReadOnly] public float duration { get; private set; }
        [field: SerializeField,ReadOnly] public float size { get; private set; }
        [field: SerializeField,ReadOnly] public int damage { get; private set; }
        [field: SerializeField] public EDamageType damageType { get; private set; }
        [field: SerializeField] public List<float> durationList { get; set; }
        [field: SerializeField] public List<int> damageList { get; set; }
        [field: SerializeField] public List<float> sizeList { get; set; }

        protected override void Upgrade()
        {
            if (lv < damageList.Count)
                damage = damageList[lv];
            if (lv < sizeList.Count)
                size = sizeList[lv];
            if (lv < durationList.Count)
                duration = durationList[lv];
        }
    }
}