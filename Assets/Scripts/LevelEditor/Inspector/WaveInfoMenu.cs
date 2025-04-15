using UnityEngine;

namespace SkyStrike
{
    namespace Editor
    {
        public class WaveInfoMenu : SubMenu<WaveDataObserver>
        {
            [SerializeField] private StringProperty waveName;
            [SerializeField] private FloatProperty delay;
            [SerializeField] private FloatProperty duration;
            [SerializeField] private BoolProperty isBossWave;

            public override void BindData()
            {
                waveName.Bind(data.name);
                delay.Bind(data.delay);
                isBossWave.Bind(data.isBoss);
                duration.Bind(data.duration);
            }
            public override void UnbindData()
            {
                waveName.Unbind();
                delay.Unbind();
                isBossWave.Unbind();
                duration.Unbind();
            }
        }
    }
}