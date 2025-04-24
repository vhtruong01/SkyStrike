using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SkyStrike.Game
{
    public class ShipCommand : Commander
    {
        public override void HandleEvent(EEntityAction action)
        {
            switch (action)
            {
                //case EEntityAction.TakeDmg:
                //    animator.SetTrigger(EAnimationType.Damage).Start();
                //    break;
                //case EEntityAction.Move:
                //    animator.SetTrigger(EAnimationType.Engine).Start();
                //    movement.Move();
                //    break;
                //case EEntityAction.Stand:
                //    animator.SetTrigger(EAnimationType.Engine).Stop();
                //    break;
                case EEntityAction.Attack:
                    //animator.SetTrigger(EAnimationType.Weapon)
                    //         .SetTotalTime(data.bulletData.timeCooldown).ResetAndStart();
                    spawner.Spawn();
                    break;
                case EEntityAction.StopAttack:
                    //animator.SetTrigger(EAnimationType.Weapon).Stop();
                    spawner.Stop();
                    break;
                    //case EEntityAction.Defend:
                    //    animator.SetTrigger(EAnimationType.Shield).Start();
                    //    break;
                    //case EEntityAction.Unprotected:
                    //    animator.SetTrigger(EAnimationType.Shield).Stop();
                    //    break;
                    //case EEntityAction.Highlight:
                    //    animator.SetTrigger(EAnimationType.HighLight).Start();
                    //    break;
                    //case EEntityAction.Die:
                    //    InterruptAllComponents();
                    //    animator.SetTrigger(EAnimationType.Destruction)
                    //             .SetFinishedAction(entity.DropItemAndDisappear).Start();
                    //    break;
                    //case EEntityAction.Disappear:
                    //    if (data.isMaintain)
                    //        animator.SetTrigger(EAnimationType.Engine).Stop();
                    //    else entity.Disappear();
                    //    break;
                    //case EEntityAction.Arrive:
                    //    StartCoroutine(MoveToNextPoint());
                    //    break;
            }
        }
        public override void Interrupt()
        {
        }
    }
}