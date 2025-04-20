using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "Ship", menuName = "Data/Ship")]
    public class ShipMetaData : ScriptableObject, IGame
    {
        public List<Skill> skills;
        [field: SerializeField] public ShieldSkill shieldSkill { get; set; }
        [field: SerializeField] public int hp { get; set; }
        [field: SerializeField] public int maxHp { get; set; }
        [field: SerializeField] public float speed { get; set; }
        [field: SerializeField] public int star { get; set; }

        public void OnEnable()
        {
            foreach (var skill in skills)
                skill.Reset();
            hp = 4;
            maxHp = 6;
            star = 0;
        }
    }
}