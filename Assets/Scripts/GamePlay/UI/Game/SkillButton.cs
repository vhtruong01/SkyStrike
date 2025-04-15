using SkyStrike.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace UI
    {
        public class SkillButton : MonoBehaviour
        {
            private static readonly Color activeColor = Color.cyan;
            private static readonly Color inactiveColor = Color.cyan.ChangeAlpha(0.2f);
            [SerializeField] private List<Image> skillPointsIcon;
            [SerializeField] private Button button;
            [SerializeField] private Image icon;
            [SerializeField] private Image border;
            private ISkill skill;
            private float elapsedTime;

            public void Start()
            {
                button.onClick.AddListener(TryUseSkill);
            }
            public void SetData(ISkill skill)
            {
                print(skill);
                this.skill = skill;
                icon.sprite = skill.sprite;
                border.color = skill.point > 0 ? activeColor : inactiveColor;
                button.GetComponent<Image>().sprite = skill.sprite;
                elapsedTime = skill.timeCooldown;
                icon.fillAmount = elapsedTime / skill.timeCooldown;
                for (int i = 0; i < 5; i++)
                {
                    float a = skill.point > i ? 1 : 0.1f;
                    skillPointsIcon[i].color = skillPointsIcon[i].color.ChangeAlpha(a);
                }
            }
            private void TryUseSkill()
            {
                if (elapsedTime >= skill.timeCooldown && skill.point > 0)
                {
                    StopAllCoroutines();
                    StartCoroutine(Cooldown());
                }
            }
            private IEnumerator Cooldown()
            {
                UseSkillPoint();
                skill.Active();
                while (elapsedTime <= skill.timeCooldown)
                {
                    elapsedTime += Time.deltaTime;
                    icon.fillAmount = elapsedTime / skill.timeCooldown;
                    yield return null;
                }
            }
            private void UseSkillPoint()
            {
                elapsedTime = 0;
                skill.point--;
                skillPointsIcon[skill.point].color = skillPointsIcon[skill.point].color.ChangeAlpha(0.1f);
                if (skill.point <= 0)
                    border.color = inactiveColor;
            }
            private void AddSkillPoint()
            {
                if (skill.point <= 0)
                    border.color = activeColor;
                skillPointsIcon[skill.point].color = skillPointsIcon[skill.point].color.ChangeAlpha(1f);
                skill.point++;
            }
        }
    }
}