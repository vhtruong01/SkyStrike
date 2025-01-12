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
            }

            public void Display(WaveData wave)
            {
                ClearAll();
                foreach(IEnemyData e in wave.enemies)
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
                enemy.data = enemyData;
                enemy.gameObject.SetActive(true);
            }
            public void RemoveEnemy(EnemyEditor enemyEditor)
            {
                enemyEditor.gameObject.SetActive(false);
                enemyPool.Release(enemyEditor);
            }
            public void Save()
            {

            }
            public void ClearAll()
            {
                foreach(EnemyEditor e in  enemyContainer.GetComponentsInChildren<EnemyEditor>()) 
                    RemoveEnemy(e);
            }
        }
    }
}