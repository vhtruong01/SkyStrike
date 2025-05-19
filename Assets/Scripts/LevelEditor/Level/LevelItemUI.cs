namespace SkyStrike.Editor
{
    public class LevelItemUI : UIElement<LevelDataObserver>
    {
        public override void BindData()
            => data.fileName.Bind(SetName);
        public override void UnbindData()
            => data.fileName.Unbind(SetName);
    }
}