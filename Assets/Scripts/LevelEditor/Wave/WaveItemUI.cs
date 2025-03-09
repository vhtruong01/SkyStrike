using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveItemUI : UIElement
        {
            [SerializeField] private TextMeshProUGUI waveName;

            public void SetName(string name) => waveName.text = name;
            public override void SetData(IEditorData data)
            {
                this.data = data;
                var waveDataObserver = this.data as WaveDataObserver;
                //
            }
        }
    }
}