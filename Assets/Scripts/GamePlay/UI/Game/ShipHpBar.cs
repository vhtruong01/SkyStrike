using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class ShipHpBar : MonoBehaviour
    {
        [SerializeField] private float dimAlpha;
        [SerializeField] private List<Image> elements;

        public void UpdateHealthDisplay(int value)
        {
            if (value < 0) return;
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
                Instantiate(lastElement, lastElement.transform.parent.transform, false);
        }
    }
}