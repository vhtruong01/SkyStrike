using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class NormalButton : MonoBehaviour
    {
        private Image img;
        private Button button;
        private UnityAction<bool> setActionValue;
        private Func<bool> getActionValue;

        public void AddListener(UnityAction<bool> setValue, Func<bool> getValue)
        {
            img = GetComponent<Image>();
            button = GetComponent<Button>();
            setActionValue = setValue;
            getActionValue = getValue;
            button.onClick.AddListener(Click);
            Display();
        }
        public void SetValue(bool value)
        {
            setActionValue?.Invoke(value);
            Display();
        }
        private void Click() => SetValue(!getActionValue.Invoke());
        private void Display()
        {
            if (getActionValue != null)
                img.color = getActionValue.Invoke() ? EditorSetting.btnSelectedColor : EditorSetting.btnDefaultColor;
        }
        public void OnEnable() => Display();
    }
}