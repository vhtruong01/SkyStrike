using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class Wave:MonoBehaviour
        {
            [SerializeField] private TextMeshProUGUI indexTxt;

            public void Init(int index)
            {
                indexTxt.text=index.ToString();

            }
        }
    }
}