using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletData : GameData<EnemyBulletMetaData, EnemyBulletData.EnemyBulletEventData>
    {
        public static readonly float maxViewAngle = 90;
        public static readonly float maxRotationAngle = 10;
        public static readonly float squaredMaxDistance = 4f;
        public static readonly float maxViewTime = 0.25f;
        public int damage => 1;
        public float elapsedTime { get; set; }
        public bool isLookingAtPlayer { get; set; }
        public float curViewTime { get; set; }
        public Color color { get; set; }
        public Vector3 velocity { get; set; }

        protected override void ChangeData(EnemyBulletEventData eventData)
        {
            metaData = eventData.metaData;
            velocity = eventData.velocity;
            elapsedTime = 0;
            curViewTime = 0;
            color = metaData.color;
            isLookingAtPlayer = metaData.isLookingAtPlayer;
        }

        public class EnemyBulletEventData : IEventData
        {
            public float angle { get; set; }
            public Vector3 velocity { get; set; }
            public Vector3 position { get; set; }
            public EnemyBulletMetaData metaData { get; set; }
        }
    }
}