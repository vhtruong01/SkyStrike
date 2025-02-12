using SkyStrike.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class ObjectInfoMenu : MonoBehaviour, ISubMenu
        {
            [SerializeField] private Vector2Property position;
            [SerializeField] private Vector2Property rotation;
            [SerializeField] private Vector2Property scale;
            [SerializeField] private TMP_InputField shipName;
            [SerializeField] private Image icon;
            [SerializeField] private TextMeshProUGUI type;
            [SerializeField] private Button add1ShipBtn;
            private IEnemyData curEnemyData;

            public void Awake()
            {
                add1ShipBtn.onClick.AddListener(CreateEnemy);
            }
            public bool CanDisplay() => curEnemyData != null;
            public void CreateEnemy()
            {
                if (curEnemyData != null)
                    MenuManager.CreateEnemy(curEnemyData);
            }
            public void Display(IData data)
            {
                if (!SetData(data)) return;
                if (curEnemyData == null) return;
                position.value = curEnemyData.position;
                scale.value = curEnemyData.scale;
                type.text = curEnemyData.type;
                icon.sprite = curEnemyData.sprite;
            }
            public bool SetData(IData data)
            {
                IEnemyData newData = data as IEnemyData;
                if (curEnemyData == newData) return false;
                curEnemyData = newData;
                return true;
            }
        }
    }
}