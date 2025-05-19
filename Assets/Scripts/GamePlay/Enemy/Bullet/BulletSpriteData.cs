using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    [CreateAssetMenu(fileName ="BulletSprites",menuName ="Data/BulletSprites")]
    public class BulletSpriteData : ScriptableObject
    {
        public List<Sprite> sprites;
    }
}