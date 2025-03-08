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
            public EnemyDataObserver objectDataObserver { get; private set; }

            public override void SetData(IData data)
            {
                var metaData = data as ObjectMetaData;
                if (metaData == null) return;
                objectDataObserver = new();
                objectDataObserver.isMetaData = true;
                objectDataObserver.metaData.SetData(metaData);
                objectDataObserver.ResetData();
                image.sprite = metaData.sprite;
                image.color = metaData.color;
                text.text = metaData.type;
            }
            public override void RemoveData() => objectDataObserver = null;
            public override IData GetData() => objectDataObserver;
        }
    }
}