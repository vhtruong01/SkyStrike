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
            [SerializeField] private Vector2Property velocity;
            [SerializeField] private FloatProperty rotation;
            //[SerializeField] private TMP_InputField enemyName;
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
                if (curEnemyData != null)
                {
                    BlindEnemy();
                    type.text = curEnemyData.metaData.data.type;
                    icon.sprite = curEnemyData.metaData.data.sprite;
                }
                else
                {
                    type.text = "";
                    icon.sprite = null;
                }
            }
            public bool SetData(IData data)
            {
                EnemyDataObserver newData = data as EnemyDataObserver;
                if (curEnemyData == newData) return false;
                UnbindEnemy();
                curEnemyData = newData;
                return true;
            }
            public void BlindEnemy()
            {
                position.Bind(curEnemyData.position.SetData);
                scale.Bind(curEnemyData.scale.SetData);
                rotation.Bind(curEnemyData.rotation.SetData);
                curEnemyData.position.Bind(position.SetValue);
                curEnemyData.scale.Bind(scale.SetValue);
                curEnemyData.rotation.Bind(rotation.SetValue);
            }
            public void UnbindEnemy()
            {
                position.Unbind();
                scale.Unbind();
                rotation.Unbind();
                if (curEnemyData == null) return;
                if (curEnemyData.isMetaData)
                    curEnemyData.ResetData();
                curEnemyData.position.Unbind(position.SetValue);
                curEnemyData.scale.Unbind(scale.SetValue);
                curEnemyData.rotation.Unbind(rotation.SetValue);
            }
            public virtual void Hide()
            {
                if (gameObject.activeSelf)
                    gameObject.SetActive(false);
            }
            public virtual void Show()
            {
                if (!gameObject.activeSelf)
                    gameObject.SetActive(true);
            }
        }
    }
}