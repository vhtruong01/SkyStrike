using UnityEngine;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Viewport : MonoBehaviour
        {
            [SerializeField] private UIGroupPool enemyGroupPool;
            [SerializeField] private Button inspectorMenuBtn;
            [SerializeField] private GameObject inspectorMenu;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private GameObject waveMenu;
            [SerializeField] private Button addObjectMenuBtn;
            [SerializeField] private GameObject addObjectMenu;
            private WaveDataObserver waveDataObserver;

            public void Awake()
            {
                inspectorMenuBtn.onClick.AddListener(() => inspectorMenu.SetActive(true));
                waveMenuBtn.onClick.AddListener(() => waveMenu.SetActive(true));
                addObjectMenuBtn.onClick.AddListener(() => addObjectMenu.SetActive(true));
                MenuManager.onCreateEnemy.AddListener(CreateEnemy);
                MenuManager.onSelectWave.AddListener(SelectWave);
                enemyGroupPool.selectDataCall = MenuManager.SelectEnemy;
            }
            public void CreateEnemy(IData data)
            {
                var enemyData = (data as EnemyDataObserver).Clone();
                if (enemyData == null) return;
                DisplayEnemy(enemyData);
                waveDataObserver.AddEnemy(enemyData);
            }
            public void SelectWave(IData data)
            {
                waveDataObserver = data as WaveDataObserver;
                enemyGroupPool.Clear();
                foreach(var enemyData in waveDataObserver.enemies)
                    DisplayEnemy(enemyData);
            }
            private void DisplayEnemy(EnemyDataObserver enemyData)
            {
                enemyGroupPool.CreateItem(out EnemyEditor enemy);
                enemy.SetData(enemyData);
            }
        }
    }
}