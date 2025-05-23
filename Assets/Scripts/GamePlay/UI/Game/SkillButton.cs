using SkyStrike.Game;
using TMPro;
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
        [SerializeField] private TextMeshProUGUI energyText;
        private SkillData skillData;
        private readonly static Color activeBorderColor = new(0.75f, 0.75f, 0.75f);
        private readonly static Color deactiveBorderColor = new(0.3f, 0.3f, 0.3f);
        private bool isLow;
        private bool isHigh;

        public void SetData(SkillData skillData)
        {
            this.skillData = skillData;
            icon.sprite = skillData.icon;
            bg.sprite = skillData.icon;
            button.onClick.AddListener(() => skillData.onActive?.Invoke());
            if (skillData.energyCost == 0)
                energyText.gameObject.SetActive(false);
            else energyText.text = skillData.energyCost.ToString();

        }
        public void UpdateTimeDisplay(float elapsedTime, float cooldown)
        {
            float percent = elapsedTime / cooldown;
            if (percent >= 1)
            {
                if (isHigh)
                    border.color = activeBorderColor;
            }
            else if (isHigh) border.color = deactiveBorderColor;
            icon.fillAmount = percent;
        }
        public void CheckEnergy(int curEnergy)
        {
            if (curEnergy < skillData.energyCost)
            {
                if (!isLow)
                {
                    border.color = energyText.color = deactiveBorderColor;
                    isLow = true;
                    isHigh = false;
                }
            }
            else
            {
                if (!isHigh && icon.fillAmount >= 1)
                {
                    border.color = energyText.color = activeBorderColor;
                    isHigh = true;
                    isLow = false;
                }
            }
        }
    }
}