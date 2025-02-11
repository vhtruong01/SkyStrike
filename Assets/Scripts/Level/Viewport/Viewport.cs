using SkyStrike.Enemy;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace SkyStrike
{
    namespace Editor
    {
        public class Viewport : MonoBehaviour
        {
            [SerializeField] private Button inspectorMenuBtn;
            [SerializeField] private Button waveMenuBtn;
            [SerializeField] private Button addObjectMenuBtn;
            [SerializeField] private GameObject enemyContainer;
            [SerializeField] private EnemyEditor enemyEditorPrefab;
            private ObjectPool<EnemyEditor> enemyPool;
            //check dead point

            public void Awake()
            {
                enemyPool = new(CreateEnemy);
                MenuManager.onCreateEnemy.AddListener(AddEnemy);
            }
            public void Display(WaveData wave)
            {
                ClearWave();
                foreach (IEnemyData e in wave.enemies)
                    AddEnemy(e);
            }
            private EnemyEditor CreateEnemy()
            {
                EnemyEditor enemy = Instantiate(enemyEditorPrefab, enemyContainer.transform, false);
                enemy.gameObject.name = "EnemyUI";
                return enemy;
            }
            public void AddEnemy(IEnemyData enemyData)
            {
                EnemyEditor enemy = enemyPool.Get();
                enemy.data = enemyData.Clone();
                enemy.gameObject.SetActive(true);
            }
            public void RemoveEnemy(EnemyEditor enemyEditor)
            {
                enemyEditor.gameObject.SetActive(false);
                enemyPool.Release(enemyEditor);
            }
            public void SaveWave()
            {

            }
            public void ClearWave()
            {
                foreach (EnemyEditor e in enemyContainer.GetComponentsInChildren<EnemyEditor>())
                    RemoveEnemy(e);
            }
        }
    }
}