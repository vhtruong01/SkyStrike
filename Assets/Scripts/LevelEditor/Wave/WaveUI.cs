using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveUI : UIElement
        {
            [SerializeField] private TextMeshProUGUI indexTxt;
            [SerializeField] private Button removeBtn;
            
            public WaveDataObserver waveDataObserver { get; private set; }

            public override void SetData(IData data)
            {
                waveDataObserver = data as WaveDataObserver;
            }
            public void SetIndex(int index)
            {
                indexTxt.text = index.ToString();
            }
        }
    }
}