using SkyStrike.Game;
using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace UI
    {
        public class GameUI : MonoBehaviour
        {
            [SerializeField] private MovingBackground bg;
            [SerializeField] private SkillButton skillButtonPrefab;
            [SerializeField] private Transform skillGroupContainer;
            [SerializeField] private TextMeshProUGUI star;
            [SerializeField] private HpBar hpBar;
            [SerializeField] private ShipData shipData;

            public void Start()
            {
                SetData();
                LoadLevel();
            }
            public void SetData()
            {
                hpBar.SetData(shipData.hp);
                star.text = shipData.star.ToString();
                foreach (var skill in shipData.skills)
                {
                    var skillUI = Instantiate(skillButtonPrefab, skillGroupContainer, false);
                    skillUI.SetData(skill);
                }
            }
            public void LoadLevel()
            {
                //
            }
        }
    }
}