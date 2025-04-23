using SkyStrike.Game;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike.Editor
{
    public class DropItemUI : UIElement<ItemData>
    {
        [SerializeField] private Image image;

        public override void SetData(ItemData data)
        {
            base.SetData(data);
            image.sprite = data.sprite;
            itemName.text = data.type.ToString();
        }
        public override void BindData() { }
        public override void UnbindData() { }
    }
}