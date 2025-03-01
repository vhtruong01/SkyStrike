using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using static UnityEditor.Progress;

namespace SkyStrike
{
    namespace Editor
    {
        public class ItemUI : MonoBehaviour, IUIElement
        {
            [SerializeField] private Image image;
            [SerializeField] private TextMeshProUGUI text;
            [SerializeField] private Image bg;
            public UnityEvent onClick { get; set; }
            public EnemyDataObserver enemyDataObserver { get; private set; }

            public void Awake()
            {
                onClick = new();
                bg = GetComponent<Image>();
            }
            public void Start()
            {
                onClick.AddListener(() => MenuManager.SelectItemUI(enemyDataObserver));    
            }
            public void SetData(EnemyMetaData metaData)
            {
                enemyDataObserver = new();
                enemyDataObserver.isMetaData = true;
                enemyDataObserver.metaData.SetData(metaData);
                enemyDataObserver.ResetData();
                image.sprite = metaData.sprite;
                text.text = metaData.type;
            }
            public Image GetBackground() => bg;
            public void OnPointerClick(PointerEventData eventData) => onClick.Invoke();
        }
    }
}