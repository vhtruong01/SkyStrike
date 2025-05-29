using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyStrike.Game
{
    public class LevelManager : MonoBehaviour
    {
        private readonly SystemMessengerEventData sysMessEventData = new();
        private readonly EnemyEventData enemyEventData = new();
        private readonly SpecialObjectEventData specialObjectEventData = new();
        private readonly LevelProgressEventData levelProgressEventData = new();
        [SerializeField] private GameManager gameManager;
        private Dictionary<int, EnemyBulletMetaData> bulletDataDict;
        private Dictionary<int, ObjectData> objectDataDict;
        private Coroutine coroutine;
        private LevelData levelData;
        private IObjectEventData objectEventData;
        private bool isEndGame;
        private int waveIndex = 0;
        private int enemyDieCount = 0;

        public void Awake()
        {
            bulletDataDict = new();
            objectDataDict = new();
        }
        public void Start()
        {
            if (PlayerPrefs.HasKey("testLevel"))
            {
                levelData = JsonUtility.FromJson<LevelData>(PlayerPrefs.GetString("testLevel"));
                waveIndex = PlayerPrefs.GetInt("testWaveIndex", 0);
                PlayerPrefs.DeleteKey("testLevel");
                PlayerPrefs.DeleteKey("testWaveIndex");
            }
            levelData ??= gameManager.curLevel;
            if (levelData == null) return;
            print("start game");
            isEndGame = false;
            sysMessEventData.text = levelData.name;
            EventManager.Active(sysMessEventData);
            sysMessEventData.text = $"Mission: Destroy {Mathf.RoundToInt(levelData.percentRequired * 100)}% of enemies";
            EventManager.Active(sysMessEventData);
            waveIndex -= 1;
            bulletDataDict.Clear();
            if (levelData.bullets != null)
                foreach (var bullet in levelData.bullets)
                    bulletDataDict.Add(bullet.id, bullet);
            levelProgressEventData.percentRequired = levelData.percentRequired;
            if (gameManager.curLevelIndex >= 4)
                SoundManager.PlaySound(ESound.Stage4);
            else SoundManager.PlaySound(ESound.Stage1 + gameManager.curLevelIndex);
        }
        private void OnEnable()
        {
            EventManager.Subscribe(EEventType.PlayNextWave, StartNextWave);
            EventManager.Subscribe(EEventType.LoseGame, StopGame);
            EventManager.Subscribe<EndGameEventData>(SaveLevel);
            EventManager.Subscribe<EnemyDieEventData>(UpdateProgress);
        }
        private void OnDisable()
        {
            EventManager.Unsubscribe(EEventType.PlayNextWave, StartNextWave);
            EventManager.Unsubscribe(EEventType.LoseGame, StopGame);
            EventManager.Unsubscribe<EndGameEventData>(SaveLevel);
            EventManager.Unsubscribe<EnemyDieEventData>(UpdateProgress);
        }
        private void SaveLevel(EndGameEventData eventData)
            => gameManager.SaveCurrentLevel(eventData.isWin, eventData.score, eventData.star);
        private void StartWave()
        {
            if (isEndGame) return;
            if (waveIndex >= levelData.waves.Length)
            {
                print("end game");
                if (levelData.percentRequired > 1f * enemyDieCount / levelData.enemyCount)
                    EventManager.Active(EEventType.LoseGame);
                else StartCoroutine(WinGame());
                return;
            }
            objectDataDict.Clear();
            WaveData waveData = levelData.waves[waveIndex];
            print($"wave: {waveData.name}, index: {1 + waveIndex}");
            if (waveData.isBoss)
            {
                EventManager.Active(EEventType.Warning);
                if (levelData.isUseNightBugTheme)
                {
                    SoundManager.PlaySound(ESound.NightBugTheme);
                    sysMessEventData.text = "Song: Stirring an Autumn Moon ~ Mooned Insect";
                    EventManager.Active(sysMessEventData);
                }
                else SoundManager.PlaySound(ESound.Boss);
            }
            foreach (var objectData in waveData.objectDataArr)
                objectDataDict.Add(objectData.id, objectData);
            foreach (var objectData in waveData.objectDataArr)
                CreateObject(objectData, waveData.delay);
            float waveDuration = waveData.objectDataArr.Length != 0 ? waveData.duration : 1;
            coroutine = StartCoroutine(WaitForNextWave(waveDuration));
        }
        private IEnumerator WinGame()
        {
            isEndGame = true;
            yield return new WaitForSeconds(5f);
            EventManager.Active(EEventType.WinGame);
        }
        private void StopGame()
        {
            isEndGame = true;
            StopAllCoroutines();
        }
        private void CreateObject(ObjectData objectData, float delay)
        {
            bool isEnemy = objectData.metaId > 0;
            objectEventData = isEnemy ? enemyEventData : specialObjectEventData;
            objectEventData.isMaintain = objectData.isMaintain;
            objectEventData.size = objectData.size;
            objectEventData.position = objectData.pos.SetZ(0);
            objectEventData.moveData = objectData.moveData;
            objectEventData.metaId = objectData.metaId;
            if (objectData.refId != -1)
                objectData.CopyMoveData(CloneMoveData(objectData.refId));
            if (isEnemy)
                foreach (var point in objectData.moveData.points)
                {
                    point.bulletData = bulletDataDict.GetValueOrDefault(point.bulletId);
                    if (point.bulletData != null)
                        point.bulletData.color = gameObject.RandomColor();
                }
            int randomEnemyIndex = objectData.dropItemType == EItem.None ? -1 : Random.Range(0, objectData.cloneCount + 1);
            for (int i = 0; i <= objectData.cloneCount; i++)
            {
                objectEventData.dropItemType = randomEnemyIndex == i ? objectData.dropItemType : EItem.None;
                objectEventData.delay = delay + objectData.moveData.delay + i * objectData.spawnInterval;
                EventManager.Active(objectEventData);
            }
        }
        private IEnumerator WaitForNextWave(float time)
        {
            if (time <= 0) yield break;
            yield return new WaitForSeconds(time);
            coroutine = null;
            StartNextWave();
        }
        private void StartNextWave()
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            waveIndex++;
            StartWave();
        }
        private MoveData CloneMoveData(int refId)
        {
            ObjectData refObject = objectDataDict.GetValueOrDefault(refId);
            if (refObject.refId == -1)
                return refObject.moveData.Clone();
            return CloneMoveData(refObject.refId);
        }
        private void UpdateProgress(EnemyDieEventData eventData)
        {
            enemyDieCount++;
            levelProgressEventData.percent = 1f * enemyDieCount / levelData.enemyCount;
            levelProgressEventData.score = eventData.score;
            EventManager.Active(levelProgressEventData);
        }
    }
}