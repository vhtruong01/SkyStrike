using UnityEngine;

namespace SkyStrike.Game
{
    [RequireComponent(typeof(SpriteAnimation))]
    public class EnemyShield : MonoBehaviour, IEnemyComponent, ISkill
    {
        private SpriteAnimation anim;
        public EnemyData enemyData { get; set; }
        public IObject entity { get; set; }

        public void Init()
            => anim = GetComponent<SpriteAnimation>();
        public void Active()
        {
            enemyData.shield = true;
            anim.Restart();
        }
        public void Deactive()
        {
            enemyData.shield = false;
            anim.Stop();
        }
        public void Interrupt() => Deactive();
        public void RefreshData() => anim.SetData(enemyData.metaData.shieldSprites);
    }
}