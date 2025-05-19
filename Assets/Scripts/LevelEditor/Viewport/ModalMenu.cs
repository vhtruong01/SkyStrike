using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class ModalMenu : Menu
    {
        private static ModalMenu instance;
        [SerializeField] private Button confirmBtn;
        [SerializeField] private Button cancelButton;
        [SerializeField] private TextMeshProUGUI text;
        private UnityAction confirmCall;
        private UnityAction cancelCall;

        protected override void Preprocess()
        {
            instance = this;
            confirmBtn.onClick.AddListener(ActiveAction);
            cancelButton.onClick.AddListener(CancelAction);
            Hide();
        }
        public void OnDisable()
            => confirmCall = cancelCall = null;
        private void ActiveAction()
        {
            confirmCall?.Invoke();
            Hide();
        }
        private void CancelAction()
        {
            cancelCall?.Invoke();
            Hide();
        }
        public static void Show(string msg, UnityAction call1, UnityAction call2 = null)
        {
            if (instance == null) return;
            instance.Show();
            instance.text.text = msg;
            instance.confirmCall = call1;
            instance.cancelCall = call2;
        }
    }
}