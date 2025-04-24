namespace SkyStrike.Game
{
    public interface IMagnetic : IObject
    {
        public bool isMagnetic { get; set; }
        public void HandleAffectedByGravity(UnityEngine.Vector2 dir);
    }
}