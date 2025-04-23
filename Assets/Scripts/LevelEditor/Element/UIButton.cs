using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class UIButton : MonoBehaviour, IUIElement
    {
        private Image bg;
        private Button button;
        public int? index { get; set; }
        public UnityEvent<int> onSelectUI { get; set; }

        public void Init()
        {
            bg = gameObject.GetComponent<Image>();
            onSelectUI = new();
            button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(SelectAndInvoke);
        }
        public Image GetBackground() => bg;
        public void SelectAndInvoke() => onSelectUI.Invoke(index.Value);
    }
}