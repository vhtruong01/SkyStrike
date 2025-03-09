using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class AddObjectItemUI : UIElement
        {
            [SerializeField] private Image image;
            [SerializeField] private TextMeshProUGUI text;

            public override void SetData(IEditorData data)
            {
                var metaData = data as ObjectMetaData;
                if (metaData == null) return;
                ObjectDataObserver objectDataObserver = new();
                objectDataObserver.isMetaData = true;
                objectDataObserver.metaData.SetData(metaData);
                objectDataObserver.ResetData();
                image.sprite = metaData.sprite;
                image.color = metaData.color;
                text.text = metaData.type;
                this.data = objectDataObserver;
            }
        }
    }
}