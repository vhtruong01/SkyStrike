using UnityEngine;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public interface IMoveableElement : IDragHandler
        {
            public Vector2 GetScaledPosition(Vector2 pos);
        }
    }
}