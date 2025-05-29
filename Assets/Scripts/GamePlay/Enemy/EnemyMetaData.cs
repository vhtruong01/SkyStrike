using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public enum EnemyType
    {
        Bomber = 0,
        Boss,
        Creep,
        Elite,
        Fighter,
        Subboss,
        Support,
        Torpedo,
        Asteroid,
    }
    public enum EnemyRace
    {
        Klaed = 1,
        Nairan,
        Nautolan
    }
    [CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemy")]
    public class EnemyMetaData : ObjectMetaData, IMetaData, IGame
    {
        private static readonly float hpCoefficient = 1.15f;
        private static readonly int minHp = 1250;
        [SerializeField] private EnemyRace race;
        [field: SerializeField] public EnemyType type { get; private set; }
        [field: SerializeField] public bool isWeakEnemy { get; private set; }
        [field: SerializeField] public ESound dieSoundType { get; private set; }
        public override int id => (int)race * 10 + (int)type;
        public int star { get; private set; }
        public int score { get; private set; }
        public int maxHp { get; private set; }
        public int energy { get; private set; }
        public int exp { get; private set; }
        [field: SerializeField] public List<Sprite> destructionSprites { get; private set; }
        [field: SerializeField] public List<Sprite> engineSprites { get; private set; }
        [field: SerializeField] public List<Sprite> shieldSprites { get; private set; }
        [field: SerializeField] public List<Sprite> weaponSprites { get; private set; }
        [field: SerializeField] public BulletAssetData bulletSprites { get; private set; }

        public override bool isCount => type != EnemyType.Asteroid;
        public void OnEnable()
        {
            star = type switch
            {
                EnemyType.Creep => 3,
                EnemyType.Support => 4,
                EnemyType.Bomber => 6,
                EnemyType.Fighter => 8,
                EnemyType.Torpedo => 10,
                EnemyType.Elite => 13,
                EnemyType.Subboss => 15,
                EnemyType.Boss => 26,
                _ => 0,
            };
            if (star != 0)
                maxHp = (int)(minHp * Mathf.Pow(hpCoefficient, star) * star);
            else maxHp = minHp * 15;
            score = maxHp / 100 * 10;
            energy = star;
            exp = maxHp / 200;
            Debug.Log($"Type: {race} {type}, star: {star}, hp: {maxHp}, score: {score}, energy: {energy}, exp {exp}");
        }
        public bool CanHighlight()
        {
            if (type == EnemyType.Boss || type == EnemyType.Subboss || type == EnemyType.Elite)
                return false;
            return true;
        }
        public override string GetName() => race.ToString() + " " + type.ToString();
    }
}