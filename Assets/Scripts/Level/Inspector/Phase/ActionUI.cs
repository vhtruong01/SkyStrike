using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ActionUI : MonoBehaviour
        {
            [SerializeField] private TextMeshProUGUI txt1;
            [SerializeField] private TextMeshProUGUI txt2;
            public IAction action {  get; set; }
            private Button button;

            public void Display()
            {

            }
        }
    }
}