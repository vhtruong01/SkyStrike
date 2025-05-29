using SkyStrike.Game;
using System.Collections.Generic;

namespace SkyStrike.Editor
{
    public class LevelDataObserver : IDataList<WaveDataObserver>, IDataList<BulletDataObserver>, IEditorData<LevelData, LevelDataObserver>
    {
        private int curBulletId;
        private List<WaveDataObserver> waveList;
        private List<BulletDataObserver> bulletList;
        private readonly Dictionary<int, BulletDataObserver> bulletDict;
        public DataObserver<string> fileName { get; private set; }
        public DataObserver<int> starRating { get; private set; }
        public DataObserver<string> levelName { get; private set; }
        public DataObserver<bool> isUseNightBugTheme { get; private set; }

        public LevelDataObserver() : this(null) { }
        public LevelDataObserver(LevelData levelData)
        {
            starRating = new();
            bulletDict = new();
            levelName = new();
            fileName = new();
            isUseNightBugTheme = new();
            ImportData(levelData);
        }
        public void GetList(out List<WaveDataObserver> list) => list = waveList;
        public void CreateEmpty(out WaveDataObserver newWave)
        {
            newWave = new();
            Add(newWave);
        }
        public void Add(WaveDataObserver data) => waveList.Add(data);
        public void Remove(WaveDataObserver data) => waveList.Remove(data);
        public void Remove(int index, out WaveDataObserver data)
        {
            data = waveList[index];
            waveList.RemoveAt(index);
        }
        public void Set(int index, WaveDataObserver data) => waveList[index] = data;
        public void GetList(out List<BulletDataObserver> list) => list = bulletList;
        public void CreateEmpty(out BulletDataObserver data)
        {
            data = new();
            Add(data);
        }
        public void Add(BulletDataObserver data)
        {
            bulletList.Add(data);
            if (data.id == BulletDataObserver.UNDEFINED_ID)
                data.id = ++curBulletId;
            bulletDict.Add(data.id, data);
        }
        public void Remove(BulletDataObserver data)
        {
            bulletList.Remove(data);
            bulletDict.Remove(data.id);
        }
        public void Remove(int index, out BulletDataObserver data)
        {
            data = bulletList[index];
            bulletList.RemoveAt(index);
            bulletDict.Remove(data.id);
        }
        public void Set(int index, BulletDataObserver data)
            => bulletList[index] = data;
        public LevelData ExportData()
        {
            LevelData levelData = new()
            {
                name = levelName.data,
                starRating = starRating.data,
                waves = new WaveData[waveList.Count],
                curBulletId = curBulletId,
                isUseNightBugTheme = isUseNightBugTheme.data,
                bullets = new EnemyBulletMetaData[bulletList.Count]
            };
            levelData.enemyCount = 0;
            for (int i = 0; i < waveList.Count; i++)
            {
                levelData.waves[i] = waveList[i].ExportData();
                levelData.enemyCount += waveList[i].GetEnemyCount();
            }
            for (int i = 0; i < bulletList.Count; i++)
                levelData.bullets[i] = bulletList[i].ExportData();
            return levelData;
        }
        public void ImportData(LevelData levelData)
        {
            waveList = new();
            bulletList = new();
            if (levelData == null)
            {
                CreateEmpty(out WaveDataObserver _);
                return;
            }
            fileName.SetData(levelData.fileName);
            starRating.SetData(levelData.starRating);
            levelName.SetData(levelData.name);
            isUseNightBugTheme.SetData(levelData.isUseNightBugTheme);
            curBulletId = levelData.curBulletId;
            if (levelData.waves != null)
                for (int i = 0; i < levelData.waves.Length; i++)
                    Add(new WaveDataObserver(levelData.waves[i]));
            if (levelData.bullets != null)
                for (int i = 0; i < levelData.bullets.Length; i++)
                    Add(new BulletDataObserver(levelData.bullets[i]));
        }
        public LevelDataObserver Clone() => null;
        public bool IsEmpty()
        {
            for (int i = 0; i < waveList.Count; i++)
                if (!waveList[i].IsEmpty()) return false;
            if (bulletList.Count > 0) return false;
            return true;
        }
    }
}