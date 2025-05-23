using UnityEngine;

namespace SkyStrike.Game
{
    public class EnemyBulletData : GameData<EnemyBulletMetaData, EnemyBulletEventData>
    {
        public static readonly float maxViewAngle = 90;
        public static readonly float maxRotationAngle = 10;
        public static readonly float squaredMaxDistance = 4f;
        public static readonly float maxViewTime = 0.25f;
        private float defaultSpeed;
        private int stateIndex;
        public int damage => 1;
        public float elapsedTime { get; set; }
        public float stateDuration { get; set; }
        public float startScale { get; private set; }
        public float endScale { get; private set; }
        public float speed { get; set; }
        public Vector3 velocity { get; set; }
        public Color color { get; private set; }
        public EnemyBulletMetaData.BulletStateData[] states { get; private set; }
        public BulletAssetData asset { get; private set; }

        protected override void ChangeData(EnemyBulletEventData eventData)
        {
            metaData = eventData.metaData;
            velocity = eventData.velocity;
            asset = eventData.asset;
            color = metaData.color;
            defaultSpeed = metaData.speed;
            states = metaData.states;
            stateIndex = 0;
            if (!metaData.isUseState || !ChangeState())
            {
                elapsedTime = 0;
                stateDuration = metaData.lifetime;
                speed = defaultSpeed;
                startScale = endScale = metaData.size;
                transform.localScale = Vector3.one * startScale;
                velocity *= defaultSpeed;
                Rotate();
            }
        }
        public bool ChangeState()
        {
            if (states == null || stateIndex >= states.Length)
                return false;
            var stateData = states[stateIndex];
            float angle = (stateData.isAuto ? Vector2.SignedAngle(velocity, Ship.pos - transform.position) : stateData.rotation) * Mathf.Deg2Rad;
            if (angle != 0)
            {
                float sin = Mathf.Sin(angle);
                float cos = Mathf.Cos(angle);
                velocity = new(velocity.x * cos - velocity.y * sin, velocity.x * sin + velocity.y * cos, velocity.z);
            }
            elapsedTime = 0;
            stateDuration = stateData.duration;
            speed = stateData.coef * defaultSpeed;
            startScale = stateData.scale;
            Rotate();
            stateIndex++;
            if (stateIndex >= states.Length)
                endScale = startScale;
            else endScale = states[stateIndex].scale;
            return true;
        }
        public void SetVelocityAndCancelState(Vector3 velo)
        {
            velocity = velo;
            states = null;
            Rotate();
        }
        public void Rotate()
            => transform.eulerAngles = transform.eulerAngles.SetZ(Vector2.SignedAngle(Vector2.up, velocity));
    }
}