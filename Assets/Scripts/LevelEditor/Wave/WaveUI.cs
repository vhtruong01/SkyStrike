using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveUI : UIElement
        {
            [SerializeField] private TextMeshProUGUI waveName;   
            public WaveDataObserver waveDataObserver { get; private set; }

            public override void SetData(IData data)
            {
                waveDataObserver = data as WaveDataObserver;
            }
            public void SetName(string name)
            {
                waveName.text = name;
            }
        }
    }
}