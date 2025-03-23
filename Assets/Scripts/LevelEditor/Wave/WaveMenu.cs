namespace SkyStrike
{
    namespace Editor
    {
        public class WaveMenu : Menu, IElementContainer<WaveDataObserver>
        {
            private LevelDataObserver levelDataObserver;
            private WaveItemList waveUIGroupPool;
            private WaveDataObserver waveDataObserver;

            public override void Awake()
            {
                base.Awake();
                EventManager.onSelectLevel.AddListener(SelectLevel);
            }
            public override void Init()
            {
                waveUIGroupPool = gameObject.GetComponent<WaveItemList>();
                waveUIGroupPool.Init(EventManager.SelectWave);
            }
            protected override void CreateObject(ObjectDataObserver data) => waveDataObserver.Add(data);
            protected override void SelectObject(ObjectDataObserver data) { }
            protected override void RemoveObject(ObjectDataObserver data) => waveDataObserver.Remove(data);
            protected override void SelectWave(WaveDataObserver data) => waveDataObserver = data;
            protected void SelectLevel(LevelDataObserver data)
            {
                levelDataObserver = data;
                waveUIGroupPool.DisplayDataList();
            }
            public IDataList<WaveDataObserver> GetDataList() => levelDataObserver;
        }
    }
}