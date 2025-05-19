using SkyStrike.UI;
using UnityEngine;

namespace SkyStrike.Game
{
    public abstract class Skill<T> : MonoBehaviour, IShipComponent, ISkill where T : SkillData
    {
        private readonly static NotiEventData notiEventData = new();
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
        public void Upgrade()
        {
            if (skillData.lv > 0)
            {
                notiEventData.notiType = ENoti.Safe;
                notiEventData.title = skillData.skillName;
                notiEventData.message = "Level up";
                notiEventData.sprite = skillData.icon;
                EventManager.ActiveUIEvent(notiEventData);
            }
            UpgradeStat();
        }
        protected abstract void UpgradeStat();
        public virtual void Interrupt() => Deactive();
    }
}