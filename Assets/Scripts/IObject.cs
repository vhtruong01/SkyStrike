namespace SkyStrike
{
    public interface IObject
    {
        public UnityEngine.GameObject gameObject { get; }
        public bool isActive => gameObject.activeSelf;
    }
}