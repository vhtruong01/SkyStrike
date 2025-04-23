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
    }
    public enum EnemyRace
    {
        Klaed = 1,
        Nairan,
        Nautolan
    }
    [CreateAssetMenu(fileName = "EnemyMetaData", menuName = "Data/EnemyMetaData")]
    public class EnemyMetaData : ScriptableObject, IGame
    {
        private static readonly float coefficient = 1.05f;
        private static readonly int minHp = 350;
        [SerializeField] private EnemyRace race;
        [SerializeField] private EnemyType type;
        public int id => (int)race * 10 + (int)type;
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
        public int maxHp => (int)(minHp * Mathf.Pow(coefficient, star) * star);
        [field: SerializeField] public Color color { get; private set; }
        [field: SerializeField] public Sprite sprite { get; private set; }
        [field: SerializeField] public List<Sprite> destructionSprites { get; private set; }
        [field: SerializeField] public List<Sprite> engineSprites { get; private set; }
        [field: SerializeField] public List<Sprite> shieldSprites { get; private set; }
        [field: SerializeField] public List<Sprite> weaponSprites { get; private set; }

        public bool CanHighLight()
        {
            if (type == EnemyType.Boss || type == EnemyType.Subboss || type == EnemyType.Elite)
                return false;
            return true;
        }
        public string GetName() => race.ToString() + " " + type.ToString();
    }
}