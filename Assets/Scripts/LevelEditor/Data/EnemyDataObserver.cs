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
            public DataObserver<Vector2> position { get; set; }
            public DataObserver<float> rotation { get; private set; }

            public EnemyDataObserver()
            {
                metaData = new();
                rotation = new();
                position = new();
                scale = new();
            }
            public EnemyDataObserver Clone()
            {
                EnemyDataObserver newData = new();
                newData.metaData = metaData;
                newData.rotation.data = rotation.data;
                newData.position.data = position.data;
                newData.scale.data = scale.data;
                return newData;
            }
            public void ResetData()
            {
                rotation.ResetData();
                position.ResetData();
                scale.ResetData();
            }
            public void Unbind()
            {
                if (isMetaData) ResetData();
                rotation.Unbind();
                position.Unbind();
                scale.Unbind();
            }
        }
    }
}