using SkyStrike.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class LevelIcon : MonoBehaviour
    {
        [SerializeField] private Image img;
        [SerializeField] private Image border;
        [SerializeField] private TextMeshProUGUI levelIndex;
        [SerializeField] private CanvasGroup levelInfoGroup;
        [SerializeField] private TextMeshProUGUI levelName;
        [SerializeField] private TextMeshProUGUI score;
        [SerializeField] private List<Image> stars;
        private Button btn;
        private Image bg;
        private UnityAction<LevelIcon> call;
        public bool isLock { get; set; }
        public int index { get; private set; }

        public void Awake()
        {
            btn = GetComponent<Button>();
            bg = GetComponent<Image>();
            btn.onClick.AddListener(Appear);
            Disappear();
        }
        public void Init(LevelData levelData, int index, int highscore, UnityAction<LevelIcon> call)
        {
            this.index = index;
            if (isLock)
            {
                levelIndex.color = border.color = Color.red;
                levelIndex.text = "X";
            }
            else
            {
                levelIndex.text = (index + 1).ToString();
                levelIndex.color = border.color = Color.cyan;
            }
            levelName.text = $"Stage {index + 1}: {levelData.name}";
            score.text = $"High score: {highscore}";
            for (int i = levelData.starRating; i < stars.Count; i++)
                stars[i].gameObject.SetActive(false);
            this.call = call;
        }
        public void Appear()
        {
            call?.Invoke(this);
            StopAllCoroutines();
            StartCoroutine(Display());
        }
        private IEnumerator Display()
        {
            float elapsedTime = 0;
            float time = 0.5f;
            img.gameObject.SetActive(true);
            while (elapsedTime < time)
            {
                float delta = elapsedTime / time;
                elapsedTime += Time.deltaTime;
                img.transform.localScale = Vector3.one * (1 + elapsedTime);
                img.color = img.color.ChangeAlpha(1 - delta);
                yield return null;
                bg.color = bg.color.ChangeAlpha(0.02f * delta);
            }
            elapsedTime = 0;
            time = 0.25f;
            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                levelInfoGroup.alpha = elapsedTime / time;
                yield return null;
            }
            img.gameObject.SetActive(false);
        }
        public void Disappear()
        {
            StopAllCoroutines();
            bg.color = bg.color.ChangeAlpha(0);
            img.gameObject.SetActive(false);
            levelInfoGroup.alpha = 0;
        }
    }
}