namespace SkyStrike.Game
{
    public class EnemyMovement : ObjectMovement, IEnemyComponent
    {
        private SpriteAnimation anim;
        public EnemyData enemyData { get; set; }
        protected override float scale
        {
            get => enemyData.size;
            set => enemyData.size = value;
        }

        public void Init()
        {
            anim = GetComponentInChildren<SpriteAnimation>(true);
            entityMoveData = enemyData;
        }
        public void RefreshData()
            => anim.SetData(enemyData.metaData.engineSprites);
        public override void Move()
        {
            anim.Play();
            enemyData.canMove = true;
        }
        public override void Stop()
        {
            anim.Stop();
            enemyData.canMove = false;
        }
        public void Interrupt()
        {
            StopAllCoroutines();
            Stop();
        }
    }
}