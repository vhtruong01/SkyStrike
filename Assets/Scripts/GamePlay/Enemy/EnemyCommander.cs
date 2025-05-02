using System.Collections;

namespace SkyStrike.Game
{
    public class EnemyCommander : Commander, IEnemyComponent
    {
        private IEnemyComponent[] comps;
        public EnemyData enemyData { get; set; }

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
                comp.UpdateData();
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
                //bool shield = !data.isImmortal && point.shield;
                //if (data.shield != shield)
                //{
                //    data.shield = shield;
                //    //notifyAction.Invoke(data.shield ? EEntityAction.Defend : EEntityAction.Unprotected);

                enemyData.isLookingAtPlayer = point.isLookingAtPlayer;
                yield return StartCoroutine(movement.Travel(point.standingTime));
                enemyData.pointIndex++;
            }
            if (!enemyData.isMaintain)
            {
                InterruptAllComponents();
                entity.Disappear();
            }
        }
        public override void Interrupt() => StopAllCoroutines();
        public override void Init() { }
        public void UpdateData() { }
    }
}