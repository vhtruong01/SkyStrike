using SkyStrike.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private MovingBackground movingBg;
        [SerializeField] private SkillButton skillButtonPrefab;
        [SerializeField] private Transform skillGroupContainer;
        [SerializeField] private GameObject uiContent;
        [SerializeField] private TextMeshProUGUI star;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private TextMeshProUGUI time;
        [SerializeField] private HpBar hpBar;
        [SerializeField] private Button startButton;
        [SerializeField] private Slider levelProcess;
        [SerializeField] private ShipData shipData;
        private Material startBtnMaterial;
        private List<SkillButton> skillButtons;


        public void Awake()
        {
            levelProcess.value = 0;
            uiContent.SetActive(false);
            startBtnMaterial = startButton.GetComponent<Image>().material;
            startButton.onClick.AddListener(() =>
            {
                startButton.gameObject.SetActive(false);
                EventManager.Active(EEventSysType.PrepareGame);
                StartCoroutine(CountTime());
            });
            skillButtons = new();
        }
        public IEnumerator Start()
        {
            shipData.onHealthChanged = hpBar.UpdateHealthDisplay;
            shipData.onCollectStar = UpdateStarDisplay;
            hpBar.UpdateHealthDisplay(shipData.health);
            UpdateStarDisplay(shipData.totalStar);
            foreach (var skill in shipData.skillDataList)
            {
                if (skill.hide) continue;
                var skillUI = Instantiate(skillButtonPrefab, skillGroupContainer, false);
                skillUI.name = skill.skillName;
                skillUI.SetData(skill);
                skillButtons.Add(skillUI);
                skill.onCooldown = skillUI.UpdateTimeDisplay;
                skillUI.UpdateTimeDisplay(skill.elapsedTime, skill.cooldown);
            }
            movingBg.enabled = false;
            float elapsedTime = 0;
            float totalTime = 2f;
            while (elapsedTime < totalTime)
            {
                startBtnMaterial.SetFloat("_Threshold", elapsedTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            movingBg.enabled = true;
        }
        public IEnumerator CountTime()
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
        public void OpenEditor() => SceneSwapper.OpenEditor();
    }
}