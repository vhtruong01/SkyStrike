namespace SkyStrike
{
    public interface IObject
    {
        public UnityEngine.GameObject gameObject { get; }
        public bool activeSelf => gameObject.activeSelf;
    }
}