using SkyStrike.Game;
using UnityEngine;
using UnityEngine.Events;

namespace SkyStrike.UI
{
    public abstract class UIElement : MonoBehaviour
    {
        public UnityAction<UIElement> onDestroy { get; set; }

        public abstract void Display(UIEventData eventData);
    }
}