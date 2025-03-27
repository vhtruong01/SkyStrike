using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class NormalButton : MonoBehaviour
        {
            private Image img;
            private bool isHighlight;
            private Button button;
            private UnityAction<bool> listener;

            public void Awake()
            {
                img = GetComponent<Image>();
                button = GetComponent<Button>();
            }
            public void AddListener(UnityAction<bool> call, bool state)
            {
                Click(state);
                listener = call;
                button.onClick.AddListener(Click);
            }
            private void Click() => Click(!isHighlight);
            private void Click(bool isHighlight)
            {
                this.isHighlight = isHighlight;
                img.color = this.isHighlight ? EditorSetting.btnSelectedColor : EditorSetting.btnDefaultColor;
                listener?.Invoke(this.isHighlight);
            }
        }
    }
}