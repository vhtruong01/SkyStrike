using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ItemUI : UIElement
        {
            [SerializeField] private Image image;
            [SerializeField] private TextMeshProUGUI text;
            public EnemyDataObserver enemyDataObserver { get; private set; }

            public void Start()
            {
                onClick.AddListener(() => MenuManager.SelectItemUI(enemyDataObserver));    
            }
            public override void SetData(IData data)
            {
                var metaData = data as EnemyMetaData;
                if (metaData == null) return;
                enemyDataObserver = new();
                enemyDataObserver.isMetaData = true;
                enemyDataObserver.metaData.SetData(metaData);
                enemyDataObserver.ResetData();
                image.sprite = metaData.sprite;
                text.text = metaData.type;
            }
        }
    }
}