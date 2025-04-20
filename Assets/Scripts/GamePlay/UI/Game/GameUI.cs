using SkyStrike.Game;
using System.Collections;
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
        [SerializeField] private TextMeshProUGUI star;
        [SerializeField] private HpBar hpBar;
        [SerializeField] private Button startButton;
        [SerializeField] private Ship ship;
        [SerializeField] private MainGame game;
        private Animator uiAnimator;
        private Material startBtnMaterial;

        public void Awake()
        {
            uiAnimator = GetComponent<Animator>();
            startBtnMaterial = startButton.GetComponent<Image>().material;
            startButton.onClick.AddListener(() => StartCoroutine(StartGame()));
        }
        public IEnumerator Start()
        {
            SetShipData();
            movingBg.enabled = false;
            float elapsedTime = 0;
            float totalTime = 2f;
            while (elapsedTime < totalTime)
            {
                startBtnMaterial.SetFloat("_Threshold", elapsedTime);
                elapsedTime += Time.deltaTime ;
                yield return null;
            }
        }
        public IEnumerator StartGame()
        {
            startButton.interactable = false;
            movingBg.enabled = true;
            yield return StartCoroutine(ship.PrepareFlying());
            startButton.gameObject.SetActive(false);
            uiAnimator.SetTrigger("Appear");
            yield return StartCoroutine(ship.StartFlying());
            game.Restart();
        }
        public void SetShipData()
        {
            ship.shipData.onCollectHealth.AddListener(hpBar.SetData);
            ship.shipData.onCollectStar.AddListener(val => star.text = val.ToString());
            hpBar.SetData(ship.shipData.hp);
            star.text = ship.shipData.star.ToString();
            //foreach (var skill in ship.shipData.skills)
            //{
            //    var skillUI = Instantiate(skillButtonPrefab, skillGroupContainer, false);
            //    skillUI.SetData(skill);
            //}
        }
        public void OpenEditor() => GameManager.OpenEditor();
    }
}