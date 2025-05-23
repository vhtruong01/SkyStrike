using UnityEngine;
using SkyStrike.Game;
using UnityEngine.UI;
using UnityEngine.Events;

namespace SkyStrike.UI
{
    public class UISound : MonoBehaviour
    {
        [SerializeField] private ESound soundType;
        private Button btn;
        private UnityAction call;

        public void Awake()
        {
            call += PlaySound;
            btn = GetComponent<Button>();
            btn.onClick.AddListener(call.Invoke);
        }
        private void PlaySound()
            => SoundManager.PlaySound(soundType);
        public void AddListener(UnityAction call)
            => this.call += call;
        public void Enable(bool isEnabled)
            => btn.interactable = isEnabled;
        public void SetSoundType(ESound soundType)
            => this.soundType = soundType;
    }
}