namespace SkyStrike
{
    namespace Game
    {
        public interface IPullable : IObject, IPoolableObject
        {
            public void HandleAffectedByGravity(UnityEngine.Vector2 dir);
        }
    }
}