using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "MegaBomb", menuName = "Data/MegaBomb")]
    public class MegaBombData : SkillData
    {
        [field: SerializeField, ReadOnly] public int damage { get; private set; }
        [field: SerializeField] public EDamageType damageType { get; private set; }
        [field: SerializeField] public List<int> damageList { get; set; }

        protected override void Upgrade()
        {
            if (lv < damageList.Count)
                damage = damageList[lv];
        }
    }
}