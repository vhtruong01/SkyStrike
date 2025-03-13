using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectDataObserver : ICloneable<ObjectDataObserver>
        {
            public bool isMetaData { get; set; }
            public int id { get; set; }
            public ObjectDataObserver refData { get; set; }
            public DataObserver<ObjectMetaData> metaData { get; private set; }
            public DataObserver<Vector2> scale { get; private set; }
            public DataObserver<Vector2> velocity { get; private set; }
            public DataObserver<Vector2> position { get; private set; }
            public DataObserver<float> rotation { get; private set; }
            public DataObserver<float> delay { get; private set; }
            public DataObserver<string> name { get; private set; }
            public PhaseDataObserver phase { get; private set; }

            public ObjectDataObserver()
            {
                metaData = new();
                rotation = new();
                position = new();
                velocity = new();
                scale = new();
                delay = new();
                name = new();
                phase = new();
                id = -1;
                refData = null;
            }
            public int GetParentCount()
            {
                return 1 + (refData == null ? 0 : refData.GetParentCount());
            }
            public ObjectDataObserver Clone()
            {
                ObjectDataObserver newData = new();
                newData.metaData = metaData;
                newData.rotation.SetData(rotation.data);
                newData.position.SetData(position.data);
                newData.velocity.SetData(velocity.data);
                newData.delay.SetData(delay.data);
                newData.name.SetData(name.data);
                newData.scale.SetData(scale.data);
                newData.refData = refData;
                newData.phase = phase.Clone();
                return newData;
            }
            public void ResetData()
            {
                rotation.SetData(metaData.data.rotation);
                position.SetData(metaData.data.position);
                velocity.SetData(metaData.data.velocity);
                scale.SetData(metaData.data.scale);
                delay.ResetData();
                name.SetData(metaData.data.type);
            }
            public void UnbindAll()
            {
                rotation.UnbindAll();
                position.UnbindAll();
                velocity.UnbindAll();
                delay.UnbindAll();
                name.UnbindAll();
                scale.UnbindAll();
            }
        }
    }
}