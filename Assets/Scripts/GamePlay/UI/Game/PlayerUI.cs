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
        [SerializeField] private EnergyBar energyBar;
        [SerializeField] private Button startButton;
        [SerializeField] private ShipData shipData;
        [SerializeField] private Button cheatBtn;
        private List<SkillButton> skillButtons;
        private Coroutine clockCoroutine;

        public void Awake()
        {
            skillButtons = new();
            startButton.onClick.AddListener(() => StartCoroutine(StartGame()));
            uiContent.SetActive(false);
            cheatBtn.gameObject.SetActive(Application.isEditor);
            cheatBtn.onClick.AddListener(shipData.Cheat);
        }
        private void OnEnable()
        {
            EventManager.Subscribe(EEventType.WinGame, StopClock);
            EventManager.Subscribe(EEventType.LoseGame, StopClock);
        }
        private void OnDisable()
        {
            EventManager.Unsubscribe(EEventType.WinGame, StopClock);
            EventManager.Unsubscribe(EEventType.LoseGame, StopClock);
        }
        private void StopClock()
        {
            if (clockCoroutine != null)
                StopCoroutine(clockCoroutine);
        }
        private IEnumerator StartGame()
        {
            startButton.gameObject.SetActive(false);
            EventManager.Active(EEventType.PrepareGame);
            yield return new WaitForSecondsRealtime(1.5f);
            uiContent.SetActive(true);
            shipData.RefreshSubcribers();
            EventManager.Active(EEventType.StartGame);
            yield return new WaitForSecondsRealtime(1);
            EventManager.Active(EEventType.PlayNextWave);
            clockCoroutine = StartCoroutine(CountTime());
        }
        public IEnumerator Start()
        {
            shipData.onHealthChanged += hpBar.UpdateHealthDisplay;
            shipData.onEnergyChanged += energyBar.UpdateEnergyDisplay;
            shipData.onCollectStar += UpdateStarDisplay;
            shipData.onExpChanged += UpdateExpDisplay;
            shipData.onLevelUp += LevelUp;
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
            float elapsedTime = 0;
            float totalTime = 2f;
            var startBtnMaterial = startButton.GetComponent<Image>().material;
            while (elapsedTime < totalTime)
            {
                startBtnMaterial.SetFloat("_Threshold", elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        private IEnumerator CountTime()
        {
            int totalTime = 0;
            while (true)
            {
                totalTime += 1;
                int min = totalTime / 60, sec = totalTime % 60;
                time.text = $"{(min < 10 ? "0" : "")}{min}:{(sec < 10 ? "0" : "")}{sec}";
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