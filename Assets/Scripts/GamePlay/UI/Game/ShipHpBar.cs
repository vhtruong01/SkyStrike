using SkyStrike.Game;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class ShipHpBar : MonoBehaviour
    {
        [SerializeField] private float dimAlpha;
        [SerializeField] private List<Image> elements;
        [SerializeField] private AlphaValueAnimation lowHpUI;

        public void UpdateHealthDisplay(int value)
        {
            if (value < 0) return;
            if (value == 1)
                lowHpUI.Restart();
            else if (lowHpUI.IsPlaying())
                lowHpUI.Stop();
            int n = Mathf.Min(value, elements.Count);
            for (int i = 0; i < n; i++)
            {
                elements[i].gameObject.SetActive(true);
                elements[i].color = elements[i].color.ChangeAlpha(1);
            }
            for (int i = Mathf.Max(0, n); i < elements.Count; i++)
            {
                elements[i].gameObject.SetActive(true);
                elements[i].color = elements[i].color.ChangeAlpha(dimAlpha);
            }
        }
        public void UpdateMaxHp(int maxHp)
        {
            if (maxHp == elements.Count) return;
            var lastElement = elements[^1];
            for (int i = 0; i < maxHp - elements.Count; i++)
            {
                var newElement = Instantiate(lastElement, lastElement.transform.parent.transform, false);
                newElement.color = newElement.color.ChangeAlpha(dimAlpha);
                elements.Add(newElement);
            }
        }
    }
}