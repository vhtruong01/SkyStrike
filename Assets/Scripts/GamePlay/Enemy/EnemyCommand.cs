using System.Collections;
using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyCommand : EnemyComponent
    {
        public override void Interrupt() => StopAllCoroutines();
        public void MoveToNextPoint() => StartCoroutine(Go());
        private IEnumerator Go()
        {
            MoveData moveData = data.moveData;
            data.pointIndex++;
            if (data.pointIndex < moveData.points.Length)
            {
                MoveData.Point point = moveData.points[data.pointIndex];
                EnemyBulletData bulletData = null;
                if (point.bulletDataList != null && point.bulletDataList.Length > 0)
                    bulletData = point.bulletDataList[0];
                data.bulletData = bulletData;
                data.isImmortal = point.isImmortal;
                data.shield = !data.isImmortal && point.shield;
                data.isLookingAtPlayer = point.isLookingAtPlayer;
                notifyAction?.Invoke(EEnemyAction.Attack);
                notifyAction?.Invoke(EEnemyAction.Defend);
                if (point.standingTime > 0)
                {
                    notifyAction?.Invoke(EEnemyAction.Stand);
                    yield return new WaitForSeconds(point.standingTime);
                }
                notifyAction?.Invoke(EEnemyAction.Move);
            }
            else notifyAction?.Invoke(EEnemyAction.Disappear);
        }
    }
}