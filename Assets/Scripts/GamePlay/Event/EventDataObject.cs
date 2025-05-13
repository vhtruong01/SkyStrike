using SkyStrike.UI;
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
    public class WaveEventData : IEventData
    {
        public float percent { get; set; }
        public bool isBossWave { get; set; }
    }
    public class EnemyDieEventData : IEventData
    {
        public int score { get; set; }
        public int exp { get; set; }
        public int energy { get; set; }
    }
    public class SystemMessengerEventData : IEventData
    {
        public string text { get; set; }
    }
    public class EndGameEventData : IEventData
    {
        public bool isWin { get; set; }
        public int score { set; get; }
        public int star { set; get; }
    }
    public abstract class UIEventData : IEventData
    {
        public abstract EUIType uiType { get; }
    }
    public class NotiEventData : UIEventData
    {
        public ENoti notiType { get; set; }
        public Sprite sprite { get; set; }
        public string message { get; set; }
        public override EUIType uiType => EUIType.Notification;
    }
    public class BossEventData : UIEventData
    {
        public override EUIType uiType => EUIType.HpBar;
        public EnemyData bossData { get; set; }
    }
    public class DamageVisualizerEventData : UIEventData
    {
        public override EUIType uiType => EUIType.DmgText;
        public int damage { get; set; }
        public EDamageType damageType { get; set; }
        public Vector3 position { get; set; }
    }
}