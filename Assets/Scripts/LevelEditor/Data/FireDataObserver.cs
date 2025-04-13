using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
    {
        public class FireDataObserver : IEditorData<BulletData, FireDataObserver>
        {
            public FireDataObserver() : base()
            {
                //
            }
            public FireDataObserver(BulletData fireData) : this() => ImportData(fireData);
            public FireDataObserver Clone()
            {
                FireDataObserver newAction = new();
                return newAction;
            }
            public BulletData ExportData()
            {
                BulletData fireData = new();
                //
                return fireData;
            }

            public void ImportData(BulletData data)
            {
                //
            }
        }
    }
}