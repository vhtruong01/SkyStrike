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
            public DataObserver<MetaData> metaData { get; private set; }
            public DataObserver<float> size { get; private set; }
            public DataObserver<int> cloneCount { get; private set; }
            public DataObserver<string> name { get; private set; }
            public MoveDataObserver moveData { get; private set; }
            public DataObserver<Vector2> position => moveData.First().midPos;

            public ObjectDataObserver()
            {
                metaData = new();
                name = new();
                moveData = new();
                size = new();
                cloneCount = new();
                id = NULL_OBJECT_ID;
                refId = NULL_OBJECT_ID;
                ResetData();
            }
            public ObjectDataObserver(ObjectData objectData) : this() => ImportData(objectData);
            public void SetPosition(Vector2 newPos) => moveData.First().Translate(newPos);
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
                newData.metaData.OnlySetData(metaData.data);
                newData.name.OnlySetData(name.data);
                newData.size.OnlySetData(size.data);
                newData.cloneCount.OnlySetData(cloneCount.data);
                newData.moveData = moveData.Clone();
                return newData;
            }
            public void ResetData()
            {
                moveData.delay.SetData(0);
                moveData.velocity.SetData(1);
                cloneCount.SetData(0);
                size.SetData(1);
                if (metaData.data != null)
                    name.OnlySetData(metaData.data.type);
            }
            public void UnbindAll()
            {
                moveData.delay.UnbindAll();
                moveData.velocity.UnbindAll();
                size.UnbindAll();
                cloneCount.UnbindAll();
                name.UnbindAll();
            }
            public ObjectData ExportData()
            {
                return new()
                {
                    id = id,
                    refId = refId,
                    metaId = metaData.data.id,
                    name = name.data,
                    size = size.data,
                    cloneCount = cloneCount.data,
                    moveData = moveData.ExportData()
                };
            }
            public void ImportData(ObjectData objectData)
            {
                if (objectData == null) return;
                id = objectData.id;
                refId = objectData.refId;
                metaData.OnlySetData(EventManager.GetMetaData(objectData.metaId));
                name.OnlySetData(objectData.name);
                size.OnlySetData(objectData.size);
                cloneCount.OnlySetData(objectData.cloneCount);
                moveData = new(objectData.moveData);
            }
        }
    }
}