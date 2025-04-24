using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "Ship", menuName = "Data/Ship")]
    public class ShipMetaData : ScriptableObject, IGame
    {
        public List<Skill> skills;
        [field: SerializeField] public int hp { get; set; }
        [field: SerializeField] public int maxHp { get; set; }
        [field: SerializeField] public float speed { get; set; }
        [field: SerializeField] public int star { get; set; }

        public void Reset()
        {
            foreach (var skill in skills)
                skill.Reset();
            star = 0;
            hp = 4;
            maxHp = 7;
        }
    }
}