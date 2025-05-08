using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Game
{
    public class BossHpUI : MonoBehaviour
    {
        [SerializeField] private Image shieldFill;
        private Slider slider;
        private EnemyData bossData;
        private int originalHp;

        public void Awake()
            => slider = GetComponent<Slider>();
        public void SetData(EnemyData bossData)
        {
            this.bossData = bossData;
            bossData.onHealthChanged = DisplayHp;
            bossData.onShieldActive = DisplayShield;
            originalHp = bossData.hp;
            DisplayShield(bossData.shield);
            DisplayHp(originalHp);
        }
        private void DisplayHp(int hp)
        {
            if (hp > 0)
                slider.value = 1f * hp / originalHp;
            else Destroy(gameObject);
        }
        public void OnDisable()
        {
            if (bossData != null)
            {
                bossData.onHealthChanged = null;
                bossData.onShieldActive = null;
            }
        }
        private void DisplayShield(bool shield)
            => shieldFill.gameObject.SetActive(shield);
    }
}