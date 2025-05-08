namespace SkyStrike.Game
{
    public interface IEntityMoveData
    {
        public int pointIndex { get; set; }
        public bool canMove { get; set; }
        public bool isMaintain { get; }
        public MoveData moveData { get; }
    }
    public abstract class ObjectEntityData<T> : GameData<T, ObjectEventData<T>>, IEntityMoveData where T : ObjectMetaData
    {
        public float size { get; protected set; }
        public int pointIndex { get; set; }
        public bool canMove { get; set; }
        public bool isMaintain { get; protected set; }
        public MoveData moveData { get; protected set; }

        protected override void ChangeData(ObjectEventData<T> eventData)
        {
            metaData = eventData.metaData;
            size = eventData.size;
            isMaintain = eventData.isMaintain;
            moveData = eventData.moveData;
            pointIndex = 0;
            canMove = true;
        }
    }
}