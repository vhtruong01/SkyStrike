using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class EnemyDataObserver : IData
        {
            public bool isMetaData { get; set; }
            public DataObserver<EnemyMetaData> metaData { get; private set; }
            public DataObserver<Vector2> scale { get; private set; }
            public DataObserver<Vector2> velocity { get; private set; }
            public DataObserver<Vector2> position { get; private set; }
            public DataObserver<float> rotation { get; private set; }

            public EnemyDataObserver()
            {
                metaData = new();
                rotation = new();
                position = new();
                velocity = new();
                scale = new();
            }
            public EnemyDataObserver Clone()
            {
                EnemyDataObserver newData = new();
                newData.metaData = metaData;
                newData.ResetData();
                return newData;
            }
            public void ResetData()
            {
                rotation.SetData(metaData.data.rotation);
                position.SetData(metaData.data.position);
                velocity.SetData(metaData.data.velocity);
                scale.SetData(metaData.data.scale);
            }
        }
    }
}