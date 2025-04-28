using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "Ship", menuName = "Data/Ship")]
    public class ShipMetaData : ScriptableObject, IMetaData, IGame
    {
        [field: SerializeField] public List<Skill> skills { get; private set; }
        [field: SerializeField] public int hp { get; private set; }
        [field: SerializeField] public int maxHp { get; private set; }
        [field: SerializeField] public float speed { get; private set; }
        [field: SerializeField] public float magnetRadius { get; private set; }

        public void Reset()
        {
            speed = 5;
            hp = 4;
            maxHp = 7;
            magnetRadius = 1.25f;
            foreach (var skill in skills)
                skill.Reset();
        }
    }
}