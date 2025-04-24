using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletData : GameData<EnemyBulletMetaData>
    {
        public static readonly float maxViewAngle = 90;
        public static readonly float maxRotationAngle = 10;
        public static readonly float squaredMaxDistance = 4f;
        public static readonly float maxViewTime = 0.25f;
        public Vector3 velocity { get; set; }
        public float elapsedTime { get; set; }
        public bool isLookingAtPlayer { get; set; }
        public float curViewTime { get; set; }
        public Color color { get; set; }
        public int damage => 1;

        public void SetExtraData(Vector3 velocity)
            => this.velocity = velocity;
        protected override void SetData(EnemyBulletMetaData metaData)
        {
            elapsedTime = 0;
            curViewTime = 0;
            color = metaData.color;
            isLookingAtPlayer = metaData.isLookingAtPlayer;
        }
    }
}