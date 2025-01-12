using SkyStrike.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class InspectorMenu : MonoBehaviour
        {
            [SerializeField] private Vector2Property position;
            [SerializeField] private Vector2Property rotation;
            [SerializeField] private Vector2Property scale;
            [SerializeField] private TMP_InputField shipName;
            [SerializeField] private Image icon;
            [SerializeField] private TextMeshProUGUI type;
            [SerializeField] private Button add1ShipBtn;
            private IEnemyData curEnemyData;
            private bool isLock;

            public void Awake()
            {
                add1ShipBtn.onClick.AddListener(() => MenuManager.SelectEnemy(curEnemyData));
            }
            public void OnEnable()
            {
                MenuManager.onSelectEnemy.AddListener(DisplayInfo);
            }
            public void OnDisable()
            {
                MenuManager.onSelectEnemy.RemoveListener(DisplayInfo);
            }
            public void DisplayInfo(IEnemyData data)
            {
                if (data == null)
                {
                    print("DisplayFail: data==null");
                    return;
                }
                curEnemyData = data;
                position.value = data.position;
                rotation.value = data.rotation;
                scale.value = data.scale;
                type.text = data.type;
                icon.sprite = data.sprite;
            }
        }
    }
}