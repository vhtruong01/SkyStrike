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
            public EnemyDataObserver enemyDataObserver { get; private set; }

            public override void SetData(IData data)
            {
                var metaData = data as EnemyMetaData;
                if (metaData == null) return;
                enemyDataObserver = new();
                enemyDataObserver.isMetaData = true;
                enemyDataObserver.metaData.SetData(metaData);
                enemyDataObserver.ResetData();
                image.sprite = metaData.sprite;
                image.color = metaData.color;
                text.text = metaData.type;
            }
            public override void RemoveData() => enemyDataObserver = null;
            public override IData GetData() => enemyDataObserver;
        }
    }
}