using DG.Tweening;

namespace SkyStrike.Game
{
    public class ShipCommander : Commander, IShipComponent
    {
        public ShipData data { get; set; }

        public EEntityAction test;
        private EEntityAction aa;
        public void Update()
        {
            if (aa != test)
            {
                aa = test;
                HandleEvent(aa);
            }
        }
        protected override void SetData()
        {
            data = GetComponent<ShipData>();
            foreach (var comp in GetComponentsInChildren<IShipComponent>(true))
                comp.data = data;
        }
        public override void Init()
        {
            entityEvents[EEntityAction.Stand] =
                () => animator.SetTrigger(EAnimationType.Engine).Stop();
            entityEvents[EEntityAction.Move] =
                () => animator.SetTrigger(EAnimationType.Engine).Play();
            entityEvents[EEntityAction.Attack] =
                () => animator.SetTrigger(EAnimationType.MainWeapon)
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
            entityEvents[EEntityAction.ActiveLaser] =
                () => animator.SetTrigger(EAnimationType.Laser).Play();
            entityEvents[EEntityAction.ActiveMissile] =
                () => animator.SetTrigger(EAnimationType.Missile).Play();
            entityEvents[EEntityAction.ActiveDoubleBulletWeapon] =
                () => animator.SetTrigger(EAnimationType.DoubleBulletWeapon).Play();
            entityEvents[EEntityAction.ActiveTripleBulletWeapon] =
                () => animator.SetTrigger(EAnimationType.TripleBulletWeapon).Play();
            entityEvents[EEntityAction.Invincible] =
                () => animator.SetTrigger(EAnimationType.Invincibility).Play();
            //entityEvents[EEntityAction.Arrive] =()=>
        }
        public void PrepareFlying(float duration)
            => transform.DOScale(1.25f, duration);
        public void Fly()
        {
            data.canMove = true;
            notifyAction.Invoke(EEntityAction.Move);
            notifyAction.Invoke(EEntityAction.Attack);
        }
    }
}