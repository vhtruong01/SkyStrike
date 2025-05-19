using System.Collections;

namespace SkyStrike.Game
{
    public class EnemyCommander : Commander, IEnemyComponent
    {
        private ISkill shieldSkill;
        private IEnemyComponent[] comps;
        public EnemyData enemyData { get; set; }

        public override void Init()
            => shieldSkill = GetComponentInChildren<ISkill>(true);

        protected override void SetData()
        {
            enemyData = GetComponent<EnemyData>();
            comps = entityObject.GetComponentsInChildren<IEnemyComponent>(true);
            foreach (var comp in comps)
                comp.enemyData = enemyData;
        }
        public void Reload()
        {
            foreach (var comp in comps)
                comp.RefreshData();
            StartCoroutine(Strike());
        }
        private IEnumerator Strike()
        {
            MoveData moveData = enemyData.moveData;
            while (enemyData.pointIndex < moveData.points.Length)
            {
                MoveData.Point point = moveData.points[enemyData.pointIndex];
                enemyData.bulletData = point.bulletData;
                spawner.Spawn();
                enemyData.isImmortal = point.isImmortal;
                //
                bool shield = !point.isImmortal && point.shield;
                if (enemyData.shield ^ shield)
                {
                    if (shield)
                        shieldSkill.Active();
                    else shieldSkill.Deactive();
                }
                enemyData.isLookingAtPlayer = point.isLookingAtPlayer;
                yield return StartCoroutine(movement.Travel(point.standingTime));
                enemyData.pointIndex++;
            }
            if (!enemyData.isMaintain)
            {
                InterruptAllComponents();
                (entity as IEntity)?.Disappear();
            }
        }
        public override void Interrupt() => StopAllCoroutines();
        public void RefreshData() { }
    }
}