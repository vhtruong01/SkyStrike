using static SkyStrike.Game.EnemyBulletMetaData;

namespace SkyStrike.Editor
{
    public class BulletStateDataObserver : IEditorData<BulletStateData, BulletStateDataObserver>
    {
        public DataObserver<float> scale { get; private set; }
        public DataObserver<float> rotation { get; private set; }
        public DataObserver<float> coef { get; private set; }
        public DataObserver<float> duration { get; private set; }
        public DataObserver<float> transitionDuration { get; private set; }
        public DataObserver<bool> isAuto { get; private set; }

        public BulletStateDataObserver()
        {
            scale = new();
            coef = new();
            rotation = new();
            duration = new();
            isAuto = new();
            transitionDuration = new();
            scale.SetData(1f);
            coef.SetData(1f);
            duration.SetData(1f);
        }
        public BulletStateDataObserver(BulletStateData bulletData) : this() => ImportData(bulletData);
        public BulletStateDataObserver Clone()
        {
            BulletStateDataObserver newData = new();
            newData.scale.SetData(scale.data);
            newData.coef.SetData(coef.data);
            newData.rotation.SetData(rotation.data);
            newData.isAuto.SetData(isAuto.data);
            newData.duration.SetData(duration.data);
            newData.transitionDuration.SetData(transitionDuration.data);
            return newData;
        }
        public BulletStateData ExportData()
        {
            return new()
            {
                scale = scale.data,
                coef = coef.data,
                rotation = rotation.data,
                isAuto = isAuto.data,
                duration = duration.data,
                transitionDuration = transitionDuration.data,
            };
        }
        public void ImportData(BulletStateData data)
        {
            scale.SetData(data.scale);
            coef.SetData(data.coef);
            rotation.SetData(data.rotation);
            isAuto.SetData(data.isAuto);
            duration.SetData(data.duration);
            transitionDuration.SetData(data.transitionDuration);
        }
    }
}