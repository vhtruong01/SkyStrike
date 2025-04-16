using SkyStrike.Game;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class BulletDataObserver : IEditorData<EnemyBulletData, BulletDataObserver>
        {
            public static readonly int UNDEFINED_ID = -1;
            public int id { get; set; }
            public DataObserver<string> name { get; private set; }
            public DataObserver<float> size { get; private set; }
            public DataObserver<float> velocity { get; private set; }
            public DataObserver<float> timeCooldown { get; private set; }
            public DataObserver<float> spinSpeed { get; private set; }
            public DataObserver<float> lifeTime { get; private set; }
            public DataObserver<float> angleUnit { get; private set; }
            public DataObserver<float> startAngle { get; private set; }
            public DataObserver<Vector2> spacing { get; private set; }
            public DataObserver<Vector2> position { get; private set; }
            public DataObserver<bool> isCircle { get; private set; }
            public DataObserver<bool> isStartAwake { get; private set; }
            public DataObserver<bool> isLookingAtPlayer { get; private set; }
            public DataObserver<int> amount { get; private set; }

            public BulletDataObserver()
            {
                id = UNDEFINED_ID;
                name = new();
                size = new();
                velocity = new();
                timeCooldown = new();
                spinSpeed = new();
                lifeTime = new();
                angleUnit = new();
                startAngle = new();
                spacing = new();
                position = new();
                amount = new();
                isCircle = new();
                isStartAwake = new();
                isLookingAtPlayer = new();
                size.SetData(1);
                velocity.SetData(2.5f);
                timeCooldown.SetData(2);
                amount.SetData(1);
                lifeTime.SetData(5);
                
            }
            public BulletDataObserver(EnemyBulletData bulletData) : this() => ImportData(bulletData);
            public BulletDataObserver Clone()
            {
                BulletDataObserver newData = new();
                newData.name.SetData(name.data + " clone");
                newData.size.SetData(size.data);
                newData.velocity.SetData(velocity.data);
                newData.timeCooldown.SetData(timeCooldown.data);
                newData.spinSpeed.SetData(spinSpeed.data);
                newData.lifeTime.SetData(lifeTime.data);
                newData.angleUnit.SetData(angleUnit.data);
                newData.startAngle.SetData(startAngle.data);
                newData.spacing.SetData(spacing.data);
                newData.position.SetData(position.data);
                newData.amount.SetData(amount.data);
                newData.isStartAwake.SetData(isStartAwake.data);
                newData.isCircle.SetData(isCircle.data);
                newData.isLookingAtPlayer.SetData(isLookingAtPlayer.data);
                return newData;
            }
            public EnemyBulletData ExportData()
            {
                return new()
                {
                    id = id,
                    name = name.data,
                    size = size.data,
                    velocity = velocity.data,
                    timeCooldown = timeCooldown.data,
                    spacing = new(spacing.data),
                    position = new(position.data),
                    spinSpeed = spinSpeed.data,
                    lifeTime = lifeTime.data,
                    angleUnit = angleUnit.data,
                    startAngle = startAngle.data,
                    isCircle = isCircle.data,
                    isStartAwake = isStartAwake.data,
                    isLookingAtPlayer=isLookingAtPlayer.data,
                    amount = amount.data,
                };
            }
            public void ImportData(EnemyBulletData data)
            {
                id = data.id;
                name.SetData(data.name);
                size.SetData(data.size);
                velocity.SetData(data.velocity);
                timeCooldown.SetData(data.timeCooldown);
                spacing.SetData(data.spacing.ToVector2());
                position.SetData(data.position.ToVector2());
                spinSpeed.SetData(data.spinSpeed);
                lifeTime.SetData(data.lifeTime);
                angleUnit.SetData(data.angleUnit);
                startAngle.SetData(data.startAngle);
                isCircle.SetData(data.isCircle);
                isStartAwake.SetData(data.isStartAwake);
                isLookingAtPlayer.SetData(data.isLookingAtPlayer);
                amount.SetData(data.amount);
            }
        }
    }
}