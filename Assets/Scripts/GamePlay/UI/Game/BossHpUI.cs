using SkyStrike.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Game
{
    public class BossHpUI : UIElement
    {
        [SerializeField] private Image shieldFill;
        private Slider slider;
        private EnemyData bossData;
        private int originalHp;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            canvasGroup = GetComponent<CanvasGroup>();
        }
        public override void Display(UIEventData eventData)
        {
            var data = eventData as BossEventData;
            bossData = data.bossData;
            bossData.onHealthChanged = DisplayHp;
            bossData.onShieldActive = DisplayShield;
            originalHp = bossData.hp;
            DisplayShield(bossData.shield);
            DisplayHp(originalHp);
            StartCoroutine(Appear());
        }
        private IEnumerator Appear()
        {
            float time = 0.5f;
            float elapsedTime = 0;
            while (elapsedTime < time)
            {
                elapsedTime += Time.unscaledDeltaTime;
                canvasGroup.alpha = elapsedTime / time;
                yield return null;
            }
        }
        private void DisplayHp(int hp)
        {
            if (hp > 0)
                slider.value = 1f * hp / originalHp;
            else
                onDestroy.Invoke(this);
        }
        private void OnDisable()
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