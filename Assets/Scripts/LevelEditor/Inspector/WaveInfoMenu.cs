using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveInfoMenu : SubMenu
        {
            [SerializeField] private StringProperty waveName;
            [SerializeField] private FloatProperty delay;
            [SerializeField] private BoolProperty isBossWave;
            private WaveDataObserver waveDataObserver;

            public override bool CanDisplay() => waveDataObserver != null;
            public override void Display(IEditorData data)
            {
                bool isNewData = SetData(data);
                if (!CanDisplay())
                {
                    waveDataObserver = null;
                    UnbindData();
                    Hide();
                    return;
                }
                if (isNewData)
                {
                    UnbindData();
                    BindData();
                }
            }
            public override bool SetData(IEditorData data)
            {
                var newData = data as WaveDataObserver;
                if (waveDataObserver == newData) return false;
                waveDataObserver = newData;
                return true;
            }
            public override void UnbindData()
            {
                waveName.Unbind();
                delay.Unbind();
                isBossWave.Unbind();
            }
            public override void BindData()
            {
                waveName.Bind(waveDataObserver.name);
                delay.Bind(waveDataObserver.delay);
                isBossWave.Bind(waveDataObserver.isBoss);
            }
        }
    }
}