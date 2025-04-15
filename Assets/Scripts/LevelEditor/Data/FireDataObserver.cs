using SkyStrike.Game;

namespace SkyStrike
{
    namespace Editor
    {
        public class FireDataObserver : IEditorData<EnemyBulletData, FireDataObserver>
        {
            public FireDataObserver() : base()
            {
                //
            }
            public FireDataObserver(EnemyBulletData fireData) : this() => ImportData(fireData);
            public FireDataObserver Clone()
            {
                FireDataObserver newAction = new();
                return newAction;
            }
            public EnemyBulletData ExportData()
            {
                EnemyBulletData fireData = new();
                //
                return fireData;
            }

            public void ImportData(EnemyBulletData data)
            {
                //
            }
        }
    }
}