using UnityEngine;

namespace SkyStrike.Game
{
    public abstract class Skill<T> : MonoBehaviour, IShipComponent, ISkill where T : SkillData
    {
        [SerializeField] protected T skillData;
        protected Coroutine coroutine;
        protected IAnimation anim;
        public IObject entity { get; set; }
        public ShipData shipData { get; set; }

        public virtual void Init()
        {
            anim = GetComponentInChildren<IAnimation>(true) ?? NullAnimation.Instance;
            skillData.onActive = Active;
            skillData.onUpgrade = Upgrade;
            Upgrade();
        }
        public void Update()
        {
            if (skillData.elapsedTime < skillData.cooldown)
            {
                skillData.elapsedTime += Time.deltaTime;
                skillData.UpdateCooldownDisplay();
                return;
            }
            AutoActive();
        }
        public virtual void AutoActive() { }
        public void Active()
        {
            if (skillData.elapsedTime >= skillData.cooldown)
            {
                if (coroutine != null)
                    StopCoroutine(coroutine);
                if (skillData.energyCost == 0 || shipData.UseEnergy(skillData.energyCost))
                {
                    Execute();
                    skillData.elapsedTime = 0;
                }
            }
        }
        public void Deactive()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            anim.Stop();
        }
        public abstract void Execute();
        public abstract void Upgrade();
        public void Interrupt() => Deactive();
    }
}