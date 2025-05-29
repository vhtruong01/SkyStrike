using UnityEngine;

namespace SkyStrike.Game
{
    public abstract class ObjectMetaData : ScriptableObject, IMetaData, IGame
    {
        public abstract int id { get; }
        public abstract bool isCount { get; }
        [field: SerializeField] public Color color { get; private set; } = Color.white;
        [field: SerializeField] public Sprite sprite { get; private set; }

        public abstract string GetName();
    }
}