using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        public class Skill : ScriptableObject, ISkill
        {
            [field: SerializeField] public string skillName { get; set; }
            [field: SerializeField] public float timeCooldown { get; set; }
            [field: SerializeField] public int point { get; set; }
            [field: SerializeField] public int maxPoint { get; set; }
            [field: SerializeField] public Sprite sprite { get; set; }

            public virtual void Active()
            {
                this.print("Use skill: " + skillName);
            }
            public void Reset()
            {
                point = 3;
            }
        }
    }
}