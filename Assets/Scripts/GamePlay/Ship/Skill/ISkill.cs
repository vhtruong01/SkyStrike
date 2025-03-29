using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public interface ISkill
        {
            public string skillName {  get; set; }
            public float timeCooldown { get; set; }
            public int point { get; set; }
            public int maxPoint { get; set; }
            public Sprite sprite { get; set; }

            public void Active();
        }
    }
}