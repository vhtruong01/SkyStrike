namespace SkyStrike
{
    namespace Editor
    {
        public class WaveMenu : Menu, IElementContainer<WaveDataObserver>
        {
            private LevelDataObserver levelDataObserver;
            private WaveItemList waveUIGroupPool;
            private WaveDataObserver waveDataObserver;

            public override void Init()
            {
                levelDataObserver = EventManager.GetLevel();
                waveUIGroupPool = gameObject.GetComponent<WaveItemList>();
                waveUIGroupPool.Init(EventManager.SelectWave);
                waveUIGroupPool.DisplayDataList();
            }
            protected override void CreateObject(ObjectDataObserver data) => waveDataObserver.Add(data);
            protected override void SelectObject(ObjectDataObserver data) { }
            protected override void RemoveObject(ObjectDataObserver data) => waveDataObserver.Remove(data);
            protected override void SelectWave(WaveDataObserver data) => waveDataObserver = data;
            public IDataList<WaveDataObserver> GetDataList() => levelDataObserver;
        }
    }
}