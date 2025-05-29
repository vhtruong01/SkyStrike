using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public enum ECelestialBody
    {
        Planet,
        Galaxy,
        Supernova,
        BlackHole,
        WhiteHole,
        WormHole,
    }
    [CreateAssetMenu(fileName = "SpecialObject", menuName = "Data/SpecialObject")]
    public class SpecialObjectMetaData : ObjectMetaData
    {
        public override int id => -(int)bodyType;
        [field: SerializeField] public ECelestialBody bodyType { get; private set; }
        [field: SerializeField] public string objectName { get; private set; }
        [field: SerializeField] public List<Sprite> sprites { get; private set; }

        public override bool isCount => false;

        public override string GetName() => objectName;
    }
}