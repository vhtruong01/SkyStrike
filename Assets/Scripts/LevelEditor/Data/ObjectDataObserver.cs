using SkyStrike.Game;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectDataObserver : IEditorData<ObjectData, ObjectDataObserver>
        {
            public readonly static int NULL_OBJECT_ID = -1;
            public int id { get; set; }
            public int refId { get; private set; }
            public ObjectDataObserver refData { get; private set; }
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
                id = NULL_OBJECT_ID;
                refId = NULL_OBJECT_ID;
            }
            public ObjectDataObserver(ObjectData objectData) : this() => ImportData(objectData);
            public void SetRefData(ObjectDataObserver data)
            {
                refData = data;
                refId = refData == null ? NULL_OBJECT_ID : refData.id;
            }
            public bool IsValidChild(ObjectDataObserver data)
            {
                if (data == this) return false;
                if (refData == null) return true;
                return refData.IsValidChild(data);
            }
            public int GetParentCount()
            {
                return refData == null ? 0 : (1 + refData.GetParentCount());
            }
            public ObjectDataObserver Clone()
            {
                ObjectDataObserver newData = new();
                newData.metaData.SetData(metaData.data);
                newData.rotation.SetData(rotation.data);
                newData.position.SetData(position.data);
                newData.velocity.SetData(velocity.data);
                newData.delay.SetData(delay.data);
                newData.name.SetData(name.data);
                newData.scale.SetData(scale.data);
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
            public ObjectData ExportData()
            {
                return new()
                {
                    id = id,
                    refId = refId,
                    metaId = metaData.data.id,
                    delay = delay.data,
                    name = name.data,
                    rotation = rotation.data,
                    scale = new(scale.data),
                    position = new(position.data),
                    velocity = new(velocity.data),
                    phase = phase.ExportData()
                };
            }
            public void ImportData(ObjectData objectData)
            {
                id = objectData.id;
                refId = objectData.refId;
                metaData.SetData(EventManager.GetMetaData(objectData.metaId));
                delay.SetData(objectData.delay);
                name.SetData(objectData.name);
                rotation.SetData(objectData.rotation);
                scale.SetData(objectData.scale.Get());
                position.SetData(objectData.position.Get());
                velocity.SetData(objectData.velocity.Get());
                phase.ImportData(objectData.phase);
            }
        }
    }
}