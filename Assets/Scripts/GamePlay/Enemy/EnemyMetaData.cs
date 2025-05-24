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
        private static readonly float hpCoefficient = 1.1f;
        private static readonly int minHp = 1000;
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

        public void OnEnable()
        {
            star = type switch
            {
                EnemyType.Bomber => 4,
                EnemyType.Boss => 30,
                EnemyType.Creep => 4,
                EnemyType.Elite => 17,
                EnemyType.Fighter => 8,
                EnemyType.Subboss => 25,
                EnemyType.Support => 2,
                EnemyType.Torpedo => 12,
                _ => 0,
            };
            maxHp = (int)(minHp * (1 + Mathf.Pow(hpCoefficient, star) * star));
            score = maxHp / 1000 * 1000;
            energy = star * 3;
            exp = maxHp / 100;
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