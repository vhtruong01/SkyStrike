using UnityEngine;

namespace SkyStrike.Game
{
    public interface IReflectable : IObject
    {
        public void Reflect(Vector2 normal);
    }
}