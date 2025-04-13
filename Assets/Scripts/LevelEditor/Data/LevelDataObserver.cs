using SkyStrike.Game;
using System.Collections.Generic;

namespace SkyStrike
{
    namespace Editor
    {
        public class LevelDataObserver : IDataList<WaveDataObserver>, IDataList<BulletDataObserver>, IEditorData<LevelData, LevelDataObserver>
        {
            private List<WaveDataObserver> waveList;
            private List<BulletDataObserver> bulletList;
            public DataObserver<int> star { get; private set; }
            public DataObserver<string> levelName { get; private set; }

            public LevelDataObserver() : this(null) { }
            public LevelDataObserver(LevelData levelData)
            {
                star = new();
                levelName = new();
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
            //public void Swap(int leftIndex, int rightIndex) => waveList.Swap(leftIndex, rightIndex);
            public void Set(int index, WaveDataObserver data) => waveList[index] = data;
            public void GetList(out List<BulletDataObserver> list) => list = bulletList;
            public void CreateEmpty(out BulletDataObserver data)
            {
                data = new();
                bulletList.Add(data);
                
            }
            public void Add(BulletDataObserver data) => bulletList.Add(data);
            public void Remove(BulletDataObserver data) => bulletList.Remove(data);
            public void Remove(int index, out BulletDataObserver data)
            {
                data = bulletList[index];
                bulletList.RemoveAt(index);
            }
            public void Set(int index, BulletDataObserver data)
                => bulletList[index] = data;
            public LevelData ExportData()
            {
                LevelData levelData = new()
                {
                    name = levelName.data,
                    star = star.data,
                    waves = new WaveData[waveList.Count],
                    bullets = new BulletData[bulletList.Count]
                };
                for (int i = 0; i < waveList.Count; i++)
                    levelData.waves[i] = waveList[i].ExportData();
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
                levelName.SetData(levelData.name);
                star.SetData(levelData.star);
                if (levelData.waves != null)
                    for (int i = 0; i < levelData.waves.Length; i++)
                        Add(new WaveDataObserver(levelData.waves[i]));
                if (levelData.bullets != null)
                    for (int i = 0; i < levelData.bullets.Length; i++)
                        Add(new BulletDataObserver(levelData.bullets[i]));
            }
            public LevelDataObserver Clone() => null;
        }
    }
}