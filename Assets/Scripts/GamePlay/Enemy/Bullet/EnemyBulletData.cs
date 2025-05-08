using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletData : GameData<EnemyBulletMetaData, EnemyBulletEventData>
    {
        public static readonly float maxViewAngle = 90;
        public static readonly float maxRotationAngle = 10;
        public static readonly float squaredMaxDistance = 4f;
        public static readonly float maxViewTime = 0.25f;
        public int damage => 1;
        public float lifetime { get; set; }
        public float curViewTime { get; set; }
        public bool isLookingAtPlayer { get; private set; }
        public Vector3 velocity { get; set; }
        public Color color { get; private set; }

        protected override void ChangeData(EnemyBulletEventData eventData)
        {
            metaData = eventData.metaData;
            velocity = eventData.velocity;
            lifetime = metaData.lifetime;
            curViewTime = 0;
            color = metaData.color;
            isLookingAtPlayer = metaData.isLookingAtPlayer;
        }
    }
}