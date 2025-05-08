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
        private static readonly float hpCoefficient = 1.05f;
        private static readonly int minHp = 500;
        [SerializeField] private EnemyRace race;
        [field: SerializeField] public EnemyType type { get; private set; }
        public override int id => (int)race * 10 + (int)type;
        public int star
        {
            get => type switch
            {
                EnemyType.Bomber => 1,
                EnemyType.Boss => 25,
                EnemyType.Creep => 1,
                EnemyType.Elite => 12,
                EnemyType.Fighter => 5,
                EnemyType.Subboss => 20,
                EnemyType.Support => 2,
                EnemyType.Torpedo => 9,
                _ => 0,
            };
        }
        public int score => (int)Mathf.Max(maxHp, Mathf.Round(maxHp / 1000) * 1000);
        public int maxHp => (int)(minHp * (1 + Mathf.Pow(hpCoefficient, star) * star));
        [field: SerializeField] public List<Sprite> destructionSprites { get; private set; }
        [field: SerializeField] public List<Sprite> engineSprites { get; private set; }
        [field: SerializeField] public List<Sprite> shieldSprites { get; private set; }
        [field: SerializeField] public List<Sprite> weaponSprites { get; private set; }
        [field: SerializeField] public List<Sprite> bulletSprites { get; private set; }

        public bool CanHighlight()
        {
            if (type == EnemyType.Boss || type == EnemyType.Subboss || type == EnemyType.Elite)
                return false;
            return true;
        }
        public override string GetName() => race.ToString() + " " + type.ToString();
    }
}