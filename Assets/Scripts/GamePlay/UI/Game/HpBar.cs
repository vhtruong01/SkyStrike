using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.UI
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private float dimAlpha;
        [SerializeField] private List<Image> elements;

        public void SetData(int value)
        {
            int n = Mathf.Min(value, elements.Count);
            for (int i = 0; i < n; i++)
            {
                elements[i].gameObject.SetActive(true);
                elements[i].color = elements[i].color.ChangeAlpha(1);
            }
            for (int i = n; i < elements.Count; i++)
            {
                elements[i].gameObject.SetActive(true);
                elements[i].color = elements[i].color.ChangeAlpha(dimAlpha);
            }
        }
    }
}