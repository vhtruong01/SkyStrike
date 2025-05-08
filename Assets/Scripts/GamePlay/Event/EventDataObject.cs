using UnityEngine;

namespace SkyStrike.Game
{
    public interface IEventData { }

    public class EnemyBulletEventData : IEventData
    {
        public float angle { get; set; }
        public Vector3 velocity { get; set; }
        public Vector3 position { get; set; }
        public EnemyBulletMetaData metaData { get; set; }
    }
    public class ItemEventData : IEventData
    {
        public int amount { get; set; }
        public EItem itemType { get; set; }
        public Vector3 position { get; set; }
        public ItemMetaData metaData { get; set; }
    }
    public class ShipBulletEventData : IEventData
    {
        public Vector3 velocity { get; set; }
        public Vector3 position { get; set; }
        public BulletData metaData { get; set; }
    }
    public interface IObjectEventData : IEventData
    {
        public int metaId { get; set; }
        public float size { get; set; }
        public bool isMaintain { get; set; }
        public float delay { get; set; }
        public Vector3 position { get; set; }
        public EItem dropItemType { get; set; }
        public MoveData moveData { get; set; }
    }
    public abstract class ObjectEventData<T> : IObjectEventData where T : ObjectMetaData
    {
        public int metaId { get; set; }
        public float size { get; set; }
        public bool isMaintain { get; set; }
        public float delay { get; set; }
        public Vector3 position { get; set; }
        public EItem dropItemType { get; set; }
        public MoveData moveData { get; set; }
        public T metaData { get; set; }
    }
    public class EnemyEventData : ObjectEventData<EnemyMetaData> { }
    public class SpecialObjectEventData : ObjectEventData<SpecialObjectMetaData> { }
    public class BossEventData : IEventData
    {
        public EnemyData bossData { get; set; }
    }
    public class DamageVisualizerEventData : IEventData
    {
        public int damage { get; set; }
        public EDamageType damageType { get; set; }
        public Vector3 position { get; set; }
    }
}