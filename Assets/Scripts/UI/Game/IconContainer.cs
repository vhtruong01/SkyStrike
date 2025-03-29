//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//namespace SkyStrike
//{
//    namespace UI
//    {
//        public class IconContainer : MonoBehaviour
//        {
//            [SerializeField] private float activeAlpha;
//            [SerializeField] private float inactiveAlphe;
//            [SerializeField] private Image prefab;
//            private List<Image> icons;

//            public void Create(int amount)
//            {
//                icons = new();
//                for (int i = 1; i <= amount; i++)
//                {
//                    Image icon = Instantiate(prefab, transform, false);
//                    icon.gameObject.SetActive(true);
//                    icons.Add(icon);
//                }
//            }
//            public void SetValue(int val)
//            {
//                for (int i = 0; i < icons.Count; i++)
//                {
//                    float a = i < val ? activeAlpha : inactiveAlphe;
//                    icons[i].color = icons[i].color.ChangeAlpha(a);
//                }
//            }
//        }
//    }
//}