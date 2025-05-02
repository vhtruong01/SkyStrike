namespace SkyStrike.Game
{
    public interface IMagnetic : IObject
    {
        public bool isMagnetic { get; }
        public void HandleAffectedByGravity(UnityEngine.Vector2 dir);
    }
}