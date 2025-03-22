namespace SkyStrike
{
    namespace Editor
    {
        public class WaveItemUI : UIElement<WaveDataObserver>
        {
            public override void SetName(string name)
            {
                base.SetName(name);
                data.name.OnlySetData(name);
            }
            public override void BindData()
            {
                data.name.Bind(SetName);
            }
            public override void UnbindData()
            {
                data.name.Unbind(SetName);
            }
        }
    }
}