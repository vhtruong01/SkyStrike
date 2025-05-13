using SkyStrike.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class PlayerUI : MonoBehaviour
    {
        private readonly SystemMessengerEventData sysMessEventData = new();
        [SerializeField] private SkillButton skillButtonPrefab;
        [SerializeField] private Transform skillGroupContainer;
        [SerializeField] private GameObject uiContent;
        [SerializeField] private TextMeshProUGUI star;
        [SerializeField] private TextMeshProUGUI time;
        [SerializeField] private TextMeshProUGUI lv;
        [SerializeField] private Slider precentExp;
        [SerializeField] private ShipHpBar hpBar;
        [SerializeField] private ScoreUI score;
        [SerializeField] private EnergyBar energyBar;
        [SerializeField] private Button startButton;
        [SerializeField] private ShipData shipData;
        private Material startBtnMaterial;
        private List<SkillButton> skillButtons;

        public void Awake()
        {
            skillButtons = new();
            startBtnMaterial = startButton.GetComponent<Image>().material;
            startButton.onClick.AddListener(() => StartCoroutine(StartGame()));
        }
        private IEnumerator StartGame()
        {
            startButton.gameObject.SetActive(false);
            EventManager.Active(EEventType.PrepareGame);
            yield return new WaitForSecondsRealtime(1.5f);
            EventManager.Active(EEventType.StartGame);
            yield return new WaitForSecondsRealtime(1);
            EventManager.Active(EEventType.PlayNextWave);
            uiContent.SetActive(true);
            StartCoroutine(CountTime());
        }
        public IEnumerator Start()
        {
            uiContent.SetActive(false);
            shipData.onHealthChanged = hpBar.UpdateHealthDisplay;
            shipData.onEnergyChanged += energyBar.UpdateEnergyDisplay;
            shipData.onScoreChanged = score.UpdateScoreDisplay;
            shipData.onCollectStar = UpdateStarDisplay;
            shipData.onExpChanged = UpdateExpDisplay;
            shipData.onLevelUp = LevelUp;
            foreach (var skill in shipData.skillDataList)
            {
                if (skill.hide) continue;
                var skillUI = Instantiate(skillButtonPrefab, skillGroupContainer, false);
                skill.onCooldown = skillUI.UpdateTimeDisplay;
                skillUI.name = skill.skillName;
                skillUI.SetData(skill);
                shipData.onEnergyChanged += skillUI.CheckEnergy;
                skillButtons.Add(skillUI);
            }
            shipData.RefreshSubcribers();
            float elapsedTime = 0;
            float totalTime = 2f;
            while (elapsedTime < totalTime)
            {
                startBtnMaterial.SetFloat("_Threshold", elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        private IEnumerator CountTime()
        {
            float totalTime = 0;
            while (true)
            {
                totalTime += 1;
                time.text = totalTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        private void UpdateStarDisplay(int amount)
            => star.text = amount.ToString();
        private void UpdateExpDisplay(float val)
            => precentExp.value = val;
        private void LevelUp()
        {
            lv.text = "Lv: " + shipData.lv;
            hpBar.UpdateMaxHp(shipData.maxHp);
            energyBar.UpdateMaxEnergy(shipData.energy, shipData.maxEnergy);
            if (shipData.lv > 1)
            {
                sysMessEventData.text = "Level up";
                EventManager.Active(sysMessEventData);
            }
        }
        public void OpenEditor() => SceneSwapper.OpenEditor();
    }
}