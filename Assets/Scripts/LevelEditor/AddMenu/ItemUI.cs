using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

namespace SkyStrike
{
    namespace Editor
    {
        public class ItemUI : MonoBehaviour, IPointerClickHandler, IUIElement
        {
            [SerializeField] private Image image;
            [SerializeField] private TextMeshProUGUI text;
            [SerializeField] private Image bg;
            private UnityEvent<ItemUI> onSelect;
            public EnemyDataObserver enemyDataObserver { get; private set; }

            public void Awake()
            {
                onSelect = new();
                bg = GetComponent<Image>();
            }
            public void Init(EnemyMetaData metaData, UnityAction<ItemUI> call)
            {
                enemyDataObserver = new();
                enemyDataObserver.isMetaData = true;
                enemyDataObserver.metaData.SetData(metaData);
                onSelect.AddListener(call);
            }
            public Image GetBackground() => bg;
            public void OnPointerClick(PointerEventData eventData)
            {
                onSelect.Invoke(this);
            }
        }
    }
}