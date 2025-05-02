using SkyStrike.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class SkillButton : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Button button;
        [SerializeField] private Image bg;
        [SerializeField] private Image border;

        public void SetData(SkillData skillData)
        {
            icon.sprite = skillData.icon;
            bg.sprite = skillData.icon;
            button.onClick.AddListener(() => skillData.onActive?.Invoke());
        }
        public void UpdateTimeDisplay(float elapsedTime, float cooldown)
        {
            float percent = elapsedTime / cooldown;
            if (percent >= 1)
                border.color = Color.cyan;
            else border.color = Color.red;
            icon.fillAmount = percent;
        }
    }
}