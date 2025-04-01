using System;
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
            private Button button;
            private UnityAction<bool> setActionValue;
            private Func<bool> getActionValue;


            public void Awake()
            {
                img = GetComponent<Image>();
                button = GetComponent<Button>();
            }
            public void AddListener(UnityAction<bool> setValue, Func<bool> getValue)
            {
                setActionValue = setValue;
                getActionValue = getValue;
                button.onClick.AddListener(Click);
                Display();
            }
            private void Click()
            {
                setActionValue?.Invoke(!getActionValue.Invoke());
                Display();
            }
            private void Display()
            {
                if (getActionValue != null)
                    img.color = getActionValue.Invoke() ? EditorSetting.btnSelectedColor : EditorSetting.btnDefaultColor;
            }
            public void OnEnable() => Display();
        }
    }
}