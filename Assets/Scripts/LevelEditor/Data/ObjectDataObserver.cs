using SkyStrike.Game;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectDataObserver : IEditorData<ObjectData, ObjectDataObserver>
        {
            public readonly static int NULL_OBJECT_ID = -1;
            public DataObserver<int> id { get; set; }
            public EItem dropItemType { get; set; }
            public int refId { get; private set; }
            //
            public ObjectDataObserver refData { get; private set; }
            //
            public DataObserver<MetaData> metaData { get; private set; }
            public DataObserver<float> size { get; private set; }
            public DataObserver<int> cloneCount { get; private set; }
            public DataObserver<float> spawnInterval { get; private set; }
            public DataObserver<string> name { get; private set; }
            public DataObserver<Vector2> position => moveData.First().midPos;
            public MoveDataObserver moveData { get; private set; }

            public ObjectDataObserver()
            {
                id = new();
                metaData = new();
                name = new();
                moveData = new();
                size = new();
                cloneCount = new();
                spawnInterval = new();
                refId = NULL_OBJECT_ID;
                id.OnlySetData(NULL_OBJECT_ID);
                ResetData();
            }
            public ObjectDataObserver(ObjectData objectData) : this() => ImportData(objectData);
            public void SetPosition(Vector2 newPos) => moveData.Translate(newPos);
            public void SetRefData(ObjectDataObserver data)
            {
                refData = data;
                refId = refData == null ? NULL_OBJECT_ID : refData.id.data;
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
                newData.spawnInterval.OnlySetData(spawnInterval.data);
                newData.moveData = moveData.Clone();
                return newData;
            }
            public void ResetData()
            {
                moveData.delay.OnlySetData(0);
                moveData.velocity.OnlySetData(1);
                cloneCount.OnlySetData(0);
                spawnInterval.OnlySetData(0.5f);
                size.OnlySetData(1);
                dropItemType = EItem.None;
                if (metaData.data != null)
                    name.OnlySetData(metaData.data.type);
            }
            public void UnbindAll()
            {
                id.UnbindAll();
                moveData.delay.UnbindAll();
                moveData.velocity.UnbindAll();
                size.UnbindAll();
                cloneCount.UnbindAll();
                spawnInterval.UnbindAll();
                name.UnbindAll();
            }
            public ObjectData ExportData()
            {
                return new()
                {
                    id = id.data,
                    refId = refId,
                    metaId = metaData.data.id,
                    name = name.data,
                    size = size.data,
                    cloneCount = cloneCount.data,
                    spawnInterval = spawnInterval.data,
                    dropItemType = dropItemType,
                    moveData = moveData.ExportData()
                };
            }
            public void ImportData(ObjectData objectData)
            {
                if (objectData == null) return;
                id.SetData(objectData.id);
                refId = objectData.refId;
                metaData.OnlySetData(EventManager.GetMetaData(objectData.metaId));
                name.OnlySetData(objectData.name);
                size.OnlySetData(objectData.size);
                cloneCount.OnlySetData(objectData.cloneCount);
                spawnInterval.SetData(objectData.spawnInterval);
                dropItemType = objectData.dropItemType;
                moveData = new(objectData.moveData);
            }
        }
    }
}