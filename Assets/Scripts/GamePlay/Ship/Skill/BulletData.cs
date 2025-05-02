using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public enum EShipBulletType
    {
        SingleBullet,
        DoubleBullet,
        TripleBullet,
        MissileBullet,
        MagicBullet,
    }
    [CreateAssetMenu(fileName = "Bullet", menuName = "Data/Bullet")]
    public class BulletData : SkillData
    {
        [field: SerializeField, ReadOnly] public int dmg { get; private set; }
        [field: SerializeField, ReadOnly] public float speed { get; private set; }
        [field: SerializeField, ReadOnly] public float scale { get; private set; }
        [field: SerializeField] public float lifetime { get; private set; } = 2;
        [field: SerializeField] public Sprite sprite { get; private set; }
        [field: SerializeField] public Material material { get; private set; }
        [field: SerializeField] public EShipBulletType type { get; private set; }
        [field: SerializeField] public EDamageType damageType { get; private set; }
        [field: SerializeField] public List<int> dmgList { get; private set; }
        [field: SerializeField] public List<float> speedList { get; private set; }
        [field: SerializeField] public List<float> scaleList { get; private set; }

        protected override void Upgrade()
        {
            if (lv < dmgList.Count)
                dmg = dmgList[lv];
            if (lv < cooldownList.Count)
                cooldown = cooldownList[lv];
            if (lv < speedList.Count)
                speed = speedList[lv];
            if (lv < scaleList.Count)
                scale = scaleList[lv];
        }
    }
}