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
            [SerializeField] private Vector2Property scale;
            [SerializeField] private Vector2Property rotation;
            [SerializeField] private TMP_InputField shipName;
            [SerializeField] private Image icon;
            [SerializeField] private TextMeshProUGUI type;
            [SerializeField] private Button add1ShipBtn;
            private EnemyDataObserver curEnemyData;

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
                BlindEnemy();
            }
            public bool SetData(IData data)
            {
                EnemyDataObserver newData = data as EnemyDataObserver;
                if (curEnemyData == newData) return false;
                UnblindEnemy();
                curEnemyData = newData;
                return true;
            }
            public void BlindEnemy()
            {
                if (curEnemyData == null) return;
                position.Bind(curEnemyData.position.OnlySetData);
                scale.Bind(curEnemyData.scale.OnlySetData);
                curEnemyData.position.Bind(position.SetValue);
                curEnemyData.scale.Bind(scale.SetValue);
            }
            public void UnblindEnemy()
            {
                position.Unbind();
                scale.Unbind();
                curEnemyData?.UnbindAll();
            }
        }
    }
}