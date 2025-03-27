using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SkyStrike
{
    namespace Editor
    {
        public class ExtraPoint : MonoBehaviour, IDragHandler
        {
            public UnityAction call;

            public void OnDrag(PointerEventData eventData) => call?.Invoke();
        }
    }
}