using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveUI:MonoBehaviour
        {
            [SerializeField] private TextMeshProUGUI indexTxt;

            public void Init(int index)
            {
                indexTxt.text=index.ToString();

            }
        }
    }
}