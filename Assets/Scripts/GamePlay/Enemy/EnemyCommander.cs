using System.Collections;

namespace SkyStrike.Game
{
    public class EnemyCommander : Commander, IEnemyComponent
    {
        public EnemyData data { get; set; }

        protected override void SetData()
        {
            data = GetComponent<EnemyData>();
            foreach (var comp in GetComponentsInChildren<IEnemyComponent>(true))
                comp.data = data;
        }
        public override void Init()
        {
            entityEvents[EEntityAction.Stand] =
                () => animator.SetTrigger(EAnimationType.Engine).Stop();
            entityEvents[EEntityAction.Move] =
                () => animator.SetTrigger(EAnimationType.Engine)
                              .SetStartedAction(movement.Move).Play();
            entityEvents[EEntityAction.Attack] =
                () => animator.SetTrigger(EAnimationType.MainWeapon)
                              .SetDuration(data.bulletData.timeCooldown)
                              .SetStartedAction(spawner.Spawn).Play();
            entityEvents[EEntityAction.StopAttack] =
                () => animator.SetTrigger(EAnimationType.MainWeapon)
                              .SetStoppedAction(spawner.Stop).Stop();
            entityEvents[EEntityAction.Defend] =
                () => animator.SetTrigger(EAnimationType.Shield).Play();
            entityEvents[EEntityAction.Unprotected] =
                () => animator.SetTrigger(EAnimationType.Shield).Stop();
            entityEvents[EEntityAction.TakeDamage] =
                () => animator.SetTrigger(EAnimationType.Damaged).Restart();
            entityEvents[EEntityAction.Highlight] =
                () => animator.SetTrigger(EAnimationType.Highlight).Play();
            entityEvents[EEntityAction.Disappear] =
                () => entity.Disappear();
            entityEvents[EEntityAction.Die] =
                () => animator.SetTrigger(EAnimationType.Destruction)
                              .SetStartedAction(InterruptAllComponents)
                              .SetFinishedAction(entity.DropItemAndDisappear).Play();
            entityEvents[EEntityAction.Arrive] =
                () => StartCoroutine(MoveToNextPoint());
        }
        private IEnumerator MoveToNextPoint()
        {
            MoveData moveData = data.moveData;
            data.pointIndex++;
            if (data.pointIndex < moveData.points.Length)
            {
                MoveData.Point point = moveData.points[data.pointIndex];
                if (data.bulletData != point.bulletData)
                {
                    data.bulletData = point.bulletData;
                    notifyAction.Invoke(data.bulletData != null ? EEntityAction.Attack : EEntityAction.StopAttack);
                }
                data.isImmortal = point.isImmortal;
                bool shield = !data.isImmortal && point.shield;
                if (data.shield != shield)
                {
                    data.shield = shield;
                    notifyAction.Invoke(data.shield ? EEntityAction.Defend : EEntityAction.Unprotected);
                }
                data.isLookingAtPlayer = point.isLookingAtPlayer;
                if (point.standingTime > 0)
                {
                    notifyAction.Invoke(EEntityAction.Stand);
                    yield return new UnityEngine.WaitForSeconds(point.standingTime);
                }
            }
            if (data.pointIndex < moveData.points.Length - 1)
                notifyAction.Invoke(EEntityAction.Move);
            else notifyAction.Invoke(data.isMaintain ? EEntityAction.Stand : EEntityAction.Disappear);
        }
    }
}