using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName = "BulletSprites", menuName = "Data/BulletSprites")]
    public class BulletAssetData : ScriptableObject
    {
        public ESound soundType;
        public List<Sprite> sprites;
    }
}