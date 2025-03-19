using TMPro;
using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveItemUI : UIElement
        {
            [SerializeField] private TextMeshProUGUI waveName;

            public override void SetData(IEditorData data)
            {
                var waveDataObserver = data as WaveDataObserver;
                //
                base.SetData(data);
            }
            public override void BindData()
            {
                var waveDataObserver = data as WaveDataObserver;
                waveDataObserver.name.Bind(ChangeName);
            }
            public override void UnbindData()
            {
                var waveDataObserver = data as WaveDataObserver;
                waveDataObserver.name.Unbind(ChangeName);
            }
            private void ChangeName(string name) => waveName.text = name;
        }
    }
}