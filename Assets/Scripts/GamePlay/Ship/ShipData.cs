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
        [SerializeField] private float defaultMagnetRadius = 1.25f;
        [field: SerializeField] public int maxLv { get; private set; }
        [field: SerializeField] public float speed { get; private set; }
        [field: SerializeField] public float magnetRadius { get; private set; }
        [field: SerializeField] public int recoverSpeed { get; private set; }
        [field: SerializeField] public bool invincibility { get; set; }
        [field: SerializeField] public bool shield { get; set; }
        [field: SerializeField] public bool isSpawn { get; set; }
        [field: SerializeField] public bool canMove { get; set; }
        [field: SerializeField, ReadOnly] public int lv { get; private set; }
        [SerializeField, ReadOnly] private int _hp;
        [SerializeField, ReadOnly] private int _energy;
        [SerializeField, ReadOnly] private int _star;
        [SerializeField, ReadOnly] private int _exp;
        public int score { get; set; }
        public int maxHp { get; private set; }
        public int maxExp { get; private set; }
        public int maxEnergy { get; private set; }
        public float invincibleTime { get; private set; } = 2.5f;
        public UnityAction<int> onCollectStar { get; set; }
        public UnityAction<int> onHealthChanged { get; set; }
        public UnityAction<int> onEnergyChanged { get; set; }
        public UnityAction<float> onExpChanged { get; set; }
        public UnityAction onLevelUp { get; set; }
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
                if (value < maxHp)
                {
                    _hp = value;
                    onHealthChanged?.Invoke(_hp);
                }
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
            _hp = 1;
            maxHp = defaultHp;
            _energy = 0;
            maxEnergy = defaultEnergy;
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
                    break;
                case EItem.Star1:
                    star++;
                    break;
                case EItem.Star5:
                    star += 5;
                    break;
                case EItem.Health:
                    hp++;
                    break;
                case EItem.Energy:
                    energy = maxEnergy;
                    break;
                default:
                    foreach (var skill in skillDataList)
                        if (skill.itemType == type)
                        {
                            skill.LvUp();
                            break;
                        }
                    break;
            }
        }
        private void LevelUp()
        {
            if (lv >= maxLv) return;
            lv = Mathf.Min(maxLv, lv + 1);
            // max hp, max energy,...
            maxHp++;
            maxEnergy *= 2;

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