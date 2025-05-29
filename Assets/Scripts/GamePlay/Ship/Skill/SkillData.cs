using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    public abstract class SkillData : ScriptableObject, IMetaData
    {
        [field: SerializeField] public EItem itemType { get; protected set; }
        [field: SerializeField] public bool hide { get; protected set; }
        [field: SerializeField] public string skillName { get; protected set; }
        [field: SerializeField] public int energyCost { get; protected set; }
        [field: SerializeField] public int maxLv { get; protected set; }
        [field: SerializeField] public Sprite icon { get; protected set; }
        [field: SerializeField] public List<float> cooldownList { get; protected set; }
        [field: SerializeField] public float cooldown { get; protected set; }
        [field: SerializeField, ReadOnly] public int lv { get; protected set; }
        [field: SerializeField, ReadOnly] public float elapsedTime { get; set; }
        public UnityAction<float, float> onCooldown { get; set; }
        public UnityAction onActive { get; set; }
        public UnityAction onUpgrade { get; set; }

        public virtual void ResetData()
        {
            lv = -1;
            cooldown = 1;
            onCooldown = null;
            onActive = null;
            onUpgrade = null;
            LvUp();
            elapsedTime = cooldown;
        }
        public virtual void LvUp()
        {
            lv++;
            if (lv < maxLv)
            {
                if (lv < cooldownList.Count)
                    cooldown = cooldownList[lv];
                Upgrade();
                onUpgrade?.Invoke();
            }
            else
            {
                lv = maxLv;
                elapsedTime = cooldown;
                UpdateCooldownDisplay();
            }
        }
        public void UpdateCooldownDisplay()
            => onCooldown?.Invoke(elapsedTime, cooldown);
        protected abstract void Upgrade();
    }
}