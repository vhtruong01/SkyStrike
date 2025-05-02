using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "Ship", menuName = "Data/Ship")]
    public class ShipData : ScriptableObject
    {
        [field: SerializeField] public List<SkillData> skillDataList { get; private set; }
        [field: SerializeField] public int defaultHp { get; private set; }
        [field: SerializeField] public int defaultMaxHp { get; private set; }
        [field: SerializeField] public float defaultMaxSpeed { get; private set; }
        [field: SerializeField] public float defaultMagnetRadius { get; private set; }
        [field: SerializeField] public int lv { get; private set; }
        [field: SerializeField] public int exp { get; private set; }
        [field: SerializeField] public int skillPoint { get; private set; }
        [field: SerializeField] public float magnetRadius { get; private set; }
        [field: SerializeField] public int maxHp { get; private set; }
        [field: SerializeField] public float speed { get; private set; }
        [field: SerializeField] public bool invincibility { get; set; }
        [field: SerializeField] public bool shield { get; set; }
        [field: SerializeField] public bool isSpawn { get; set; }
        [field: SerializeField] public bool canMove { get; set; }
        [SerializeField] private int _hp;
        [SerializeField] private int _star;
        public UnityAction<int> onCollectStar { get; set; }
        public UnityAction<int> onHealthChanged { get; set; }
        public int health
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
        public int totalStar
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
            skillPoint = 0;
            totalStar = 0;
            health = defaultHp;
            lv = 1;
            exp = 0;
            speed = defaultMaxSpeed;
            maxHp = defaultMaxHp;
            magnetRadius = defaultMagnetRadius;
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
                case EItem.Star1:
                    totalStar++;
                    break;
                case EItem.Star5:
                    totalStar += 5;
                    break;
                case EItem.Health:
                    health++;
                    break;
                case EItem.SkillPoint:
                    skillPoint++;
                    break;
            }
        }
    }
}