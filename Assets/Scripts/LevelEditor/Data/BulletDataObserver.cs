using SkyStrike.Game;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class BulletDataObserver : IEditorData<BulletData, BulletDataObserver>
        {
            public int id { get; set; }
            public DataObserver<string> name { get; private set; }
            public DataObserver<float> size { get; private set; }
            public DataObserver<float> velocity { get; private set; }
            public DataObserver<float> timeCooldown { get; private set; }
            public DataObserver<float> spinSpeed { get; private set; }
            public DataObserver<float> length { get; private set; }
            public DataObserver<float> angleUnit { get; private set; }
            public DataObserver<Vector2> spacing { get; private set; }
            public DataObserver<Vector2> position { get; private set; }
            public DataObserver<bool> isCircle { get; private set; }
            public DataObserver<bool> isStartAwake { get; private set; }
            public DataObserver<int> amount { get; private set; }

            public BulletDataObserver()
            {
                id = 0;
                name = new();
                size = new();
                velocity = new();
                timeCooldown = new();
                spinSpeed = new();
                length = new();
                angleUnit = new();
                spacing = new();
                position = new();
                amount = new();
                isCircle = new();
                isStartAwake = new();
            }
            public BulletDataObserver(BulletData bulletData) : this() => ImportData(bulletData);
            public BulletDataObserver Clone()
            {
                BulletDataObserver newData = new();
                newData.name.SetData(name.data + " clone");
                newData.size.SetData(size.data);
                newData.velocity.SetData(velocity.data);
                newData.timeCooldown.SetData(timeCooldown.data);
                newData.spinSpeed.SetData(spinSpeed.data);
                newData.length.SetData(length.data);
                newData.angleUnit.SetData(angleUnit.data);
                newData.spacing.SetData(spacing.data);
                newData.position.SetData(position.data);
                newData.amount.SetData(amount.data);
                newData.isStartAwake.SetData(isStartAwake.data);
                newData.isCircle.SetData(isCircle.data);
                return newData;
            }
            public BulletData ExportData()
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
                    length = length.data,
                    angleUnit = angleUnit.data,
                    isCircle = isCircle.data,
                    isStartAwake = isStartAwake.data,
                    amount = amount.data,

                };
            }
            public void ImportData(BulletData data)
            {
                id = data.id;
                name.SetData(data.name);
                size.SetData(data.size);
                velocity.SetData(data.velocity);
                timeCooldown.SetData(data.timeCooldown);
                spacing.SetData(data.spacing.ToVector2());
                position.SetData(data.position.ToVector2());
                spinSpeed.SetData(data.spinSpeed);
                length.SetData(data.length);
                angleUnit.SetData(data.angleUnit);
                isCircle.SetData(data.isCircle);
                isStartAwake.SetData(data.isStartAwake);
                amount.SetData(data.amount);
            }
        }
    }
}