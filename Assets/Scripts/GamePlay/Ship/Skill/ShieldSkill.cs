using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [CreateAssetMenu(fileName = "Shield", menuName = "Skill/Shield")]
        public class ShieldSkill : Skill
        {
            public override void Active()
            {
                base.Active();
                this.print(1);
            }
        }
    }
}