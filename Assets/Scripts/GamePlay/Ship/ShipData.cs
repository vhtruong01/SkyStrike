using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [CreateAssetMenu(fileName = "Ship", menuName = "Ship")]
        public class ShipData : ScriptableObject
        {
            public List<Skill> skills;
            public int hp;
            public int maxHp;
            public float speed;
            public int star;

            public void OnEnable()
            {
                foreach (var skill in skills)
                    skill.Reset();
                hp = 4;
                maxHp = 6;
                star = 1000;
            }
        }
    }
}