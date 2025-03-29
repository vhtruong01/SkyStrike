using UnityEngine;

namespace SkyStrike
{
    namespace Game
    {
        [CreateAssetMenu(fileName = "Laser", menuName = "Skill/Laser")]
        public class LaserSkill : Skill
        {
            public override void Active()
            {
                this.print(1);
            }
        }
    }
}