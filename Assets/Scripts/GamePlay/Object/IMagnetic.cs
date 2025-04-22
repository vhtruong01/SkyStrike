namespace SkyStrike.Game
{
    public interface IMagnetic : IObject, IPoolableObject
    {
        public bool isMagnetic { get; set; }
        public void HandleAffectedByGravity(UnityEngine.Vector2 dir);
    }
}