using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class EnergyBar : MonoBehaviour
    {
        private Slider slider;
        private TextMeshProUGUI text;
        private int maxEnergy;

        private void Awake()
        {
            slider = GetComponentInChildren<Slider>();
            text = GetComponentInChildren<TextMeshProUGUI>();
            slider.value = 0;
        }
        public void UpdateEnergyDisplay(int curVal)
        {
            slider.value = 1f * curVal / maxEnergy;
            text.text = curVal.ToString();
        }
        public void UpdateMaxEnergy(int curEnergy, int maxEnergy)
        {
            this.maxEnergy = maxEnergy;
            UpdateEnergyDisplay(curEnergy);
        }
    }
}