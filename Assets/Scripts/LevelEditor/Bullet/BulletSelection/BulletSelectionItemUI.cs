using UnityEngine;

namespace SkyStrike.Editor
{
    public class BulletSelectionItemUI : UIElement<BulletDataObserver>
    {
        public override void BindData()
            => data.name.Bind(SetName);
        public override void UnbindData()
            => data.name.Unbind(SetName);
    }
}