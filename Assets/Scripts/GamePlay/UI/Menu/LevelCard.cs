using SkyStrike.Game;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class LevelCard : MonoBehaviour
    {
        [SerializeField] private Image img;
        [SerializeField] private Button btn;
        [SerializeField] private TextMeshProUGUI levelName;
        [SerializeField] private Transform starContainer;

        public void Init(LevelData levelData, UnityAction call)
        {
            btn.onClick.AddListener(call);
            levelName.text = levelData.name;
            for (int i = 1; i <= starContainer.childCount; i++)
                if (i > levelData.starRating)
                {
                    var icon = starContainer.GetChild(i - 1).GetComponent<Image>();
                    icon.color = icon.color.ChangeAlpha(0.2f);
                }
        }
    }
}