using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveInfoMenu : SubMenu, IObserverMenu
        {
            [SerializeField] private StringProperty waveName;
            [SerializeField] private BoolProperty isBossWave;
            private WaveDataObserver waveDataObserver;

            public override bool CanDisplay() => waveDataObserver != null;
            public override void Display(IData data)
            {
            }

            public override bool SetData(IData data)
            {
                return true;
            }

            public void UnbindData()
            {
            }
            public void BindData()
            {
            }
        }
    }
}