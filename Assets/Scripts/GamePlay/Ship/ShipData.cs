using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "Ship", menuName = "Data/Ship")]
    public class ShipData : ScriptableObject
    {
        [field: SerializeField] public List<SkillData> skillDataList { get; private set; }
        [SerializeField] private int defaultHp = 7;
        [SerializeField] private int defaultExp;
        [SerializeField] private int defaultEnergy;
        [SerializeField] private int defaultRecoverSpeed;
        [SerializeField] private float defaultSpeed = 5;
        [SerializeField] private float defaultMagnetRadius = 1f;
        [field: SerializeField] public float speed { get; private set; }
        [field: SerializeField] public float magnetRadius { get; private set; }
        [field: SerializeField] public int recoverSpeed { get; private set; }
        [field: SerializeField, ReadOnly] public bool invincibility { get; set; }
        [field: SerializeField, ReadOnly] public bool shield { get; set; }
        [field: SerializeField, ReadOnly] public bool isSpawn { get; set; }
        [field: SerializeField, ReadOnly] public bool canMove { get; set; }
        [field: SerializeField, ReadOnly] public int lv { get; private set; }
        [SerializeField, ReadOnly] private int _hp;
        [SerializeField, ReadOnly] private int _energy;
        [SerializeField, ReadOnly] private int _star;
        [SerializeField, ReadOnly] private int _exp;
        public int score { get; set; }
        public int maxHp { get; private set; }
        public int maxExp { get; private set; }
        public int maxEnergy { get; private set; }
        public float invincibleTime { get; private set; } = 1f;
        public UnityAction<int> onCollectStar { get; set; }
        public UnityAction<int> onHealthChanged { get; set; }
        public UnityAction<int> onEnergyChanged { get; set; }
        public UnityAction<float> onExpChanged { get; set; }
        public UnityAction onLevelUp { get; set; }
        public int maxLv => 11;
        public int exp
        {
            get => _exp;
            set
            {
                _exp = value;
                if (_exp >= maxExp)
                {
                    _exp %= maxExp;
                    LevelUp();
                }
                onExpChanged?.Invoke(1f * _exp / maxExp);
            }
        }
        public int energy
        {
            get => _energy;
            set
            {
                var newEnergy = Mathf.Min(value, maxEnergy);
                if (newEnergy != _energy)
                {
                    onEnergyChanged?.Invoke(newEnergy);
                    _energy = newEnergy;
                }
            }
        }
        public int hp
        {
            get => _hp;
            set
            {
                _hp = value;
                onHealthChanged?.Invoke(_hp);
            }
        }
        public int star
        {
            get => _star;
            set
            {
                _star = value;
                onCollectStar?.Invoke(_star);
            }
        }
        public void ResetData()
        {
            onCollectStar = null;
            onHealthChanged = null;
            onEnergyChanged = null;
            onExpChanged = null;
            onLevelUp = null;
            lv = 1;
            score = 0;
            _star = 0;
            _exp = 0;
            _hp = maxHp = defaultHp;
            _hp = 5;
            maxEnergy = defaultEnergy;
            _energy = maxEnergy * 3 / 4;
            speed = defaultSpeed;
            maxExp = defaultExp;
            magnetRadius = defaultMagnetRadius;
            recoverSpeed = defaultRecoverSpeed;
            invincibility = false;
            shield = false;
            canMove = false;
            isSpawn = false;
            foreach (var skill in skillDataList)
                skill.ResetData();
        }
        public void CollectItem(EItem type)
        {
            switch (type)
            {
                case EItem.None:
                    return;
                case EItem.Star1:
                    star++;
                    SoundManager.PlaySound(ESound.Star1);
                    return;
                case EItem.Star5:
                    star += 5;
                    SoundManager.PlaySound(ESound.Star5);
                    return;
                case EItem.Health:
                    if (hp < maxHp)
                    {
                        hp++;
                        SoundManager.PlaySound(ESound.Health);
                    }
                    return;
                case EItem.Energy:
                    energy += maxEnergy / 3;
                    SoundManager.PlaySound(ESound.Energy);
                    return;
            }
            SoundManager.PlaySound(ESound.CollectItem);
            foreach (var skill in skillDataList)
                if (skill.itemType == type)
                {
                    skill.LvUp();
                    break;
                }
        }
        private void LevelUp()
        {
            lv++;
            maxHp = defaultHp + lv / 5;
            maxEnergy = defaultEnergy + lv * 100;
            maxExp = lv >= maxLv ? int.MaxValue : (defaultExp + lv * (int)Mathf.Pow(1.05f, lv));
            recoverSpeed = defaultRecoverSpeed + lv;
            magnetRadius = defaultMagnetRadius + lv * 0.1f;
            if (lv > 1)
                SoundManager.PlaySound(ESound.LevelUp2 + (lv - 2));
            onLevelUp?.Invoke();
        }
        public bool UseEnergy(int amount)
        {
            if (amount > _energy) return false;
            energy -= amount;
            return true;
        }
        public void RefreshSubcribers()
        {
            onLevelUp.Invoke();
            onCollectStar.Invoke(_star);
            onEnergyChanged.Invoke(_energy);
            onHealthChanged.Invoke(_hp);
            onExpChanged.Invoke(1f * _exp / maxExp);
            foreach (var skill in skillDataList)
                skill.onCooldown?.Invoke(skill.elapsedTime, skill.cooldown);
        }
    }
}